using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TerminalProject.Source_files;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace TerminalProject
{
    public partial class MainForm : Form
    {
        // Console
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        // SerialPort
        private static CustomSerialPort serialPort;
        // InData
        private ValueTuple<string, string, int> inData = new ValueTuple<string, string, int>();

        ////////////////
        // For Files
        ////////////////
        private string currentDirectoryPath;
        private string filesToSendDirectory = "Final Project Files/Files to send";
        private string filesRecievedDirectory = "Final Project Files/Files recieved";
        private string selectedFilePath = "";
        private string dataFilesPath = "";

        ////////////////
        // Radar System
        ////////////////
        private bool scanMode = false;
        private float maskedDistance =  200; // cm
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        private int WIDTH, HEIGHT, HAND;
        private int handDegMain;                                     // in degree
        private int handGreenX, handGreenY, handRedX, handRedY;      // HAND coordinate
        private const int LIM = 5, MAX_SERVO_ANGLE = 180;
        private int circleX, circleY;                                // center of the circle
        private int handStartAngle = -90;
        private Point[] linesArray = new Point[3 * LIM];

        // radar drawing
        private Bitmap bmp;
        private Pen pen = new Pen(Color.Green, 2.5f);
        private Pen[] penGreenList = new Pen[LIM];
        private Pen[] penRedList = new Pen[LIM];
        private Graphics graphics;
        private static double greenHue = Color.Green.GetHue();
        private static double greenSaturation = Color.Green.GetSaturation();
        private static double greenBrightness = Color.Green.GetBrightness();
        private Color[] greenColorList = new Color[LIM];
        private static double redHue = Color.Red.GetHue();
        private static double redSaturation = Color.Red.GetSaturation();
        private static double redBrightness = Color.Red.GetBrightness();
        private Color[] redColorList = new Color[LIM];


        /*
         * Construstor
         */
        public MainForm()
        {
            InitializeComponent();

            Console.WriteLine("Main Form Init");

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            tabControl1.Width = this.Width;

            // Get Current Directory
            currentDirectoryPath = Environment.CurrentDirectory;

            // Serial Port
            serialPort = new CustomSerialPort();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);

            try
            {
                serialPort.Open();
                setConnectingLabel(CustomSerialPort.STATUS.PORT_OK);
            }
            catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }

            // Event Hub Handlers
            EventHub.saveConfigurationsHandler += onSerialConfigurationsChanged;
            EventHub.fileSendingProgressHandler += onFileSendingProgress;

            // List View Initialization
            initializeFilesListView();

            // Set Default Masked Distance
            maskedDistanceTextBox.Text = maskedDistance.ToString();

        } // END MainForm

        /*
         *  Form1_Load
         */ 
        private void Form1_Load(object sender, EventArgs e) {
            AllocConsole();
            Console.WriteLine("Main Form Load");
            radarTextPanel.Visible = false;

            WIDTH = radarPanel.Height * 2;
            HEIGHT = radarPanel.Height;
            HAND = radarPanel.Height;

            //create Bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            //center
            circleX = radarPanel.Width / 2  + 55;
            circleY = radarPanel.Height;

            //initial degree of HAND
            handDegMain = handStartAngle;

            for (int i = 0; i < LIM-1; i++) {
                greenColorList[i] = ColorFromHSV(greenHue, greenSaturation, (1 - greenBrightness) / (LIM - 1) * i);
                redColorList[i] = ColorFromHSV(redHue, redSaturation, (1 - redBrightness) / (LIM - 1) * i);
            }
            greenColorList[0] = Color.Green;
            greenColorList[LIM - 1] = Color.Black;
            redColorList[0] = Color.Red;
            redColorList[LIM - 1] = Color.Black;

            for (int i = 0; i < LIM; i++)
            {
                penGreenList[i] = new Pen(greenColorList[i], 2.5f);
                penRedList[i] = new Pen(redColorList[i], 2.5f);
            }


            //timer
            t.Interval = 50; //in millisecond
           // t.Tick += new EventHandler(this.t_Tick);

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////           Main Methods
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
         * Radar Visualisation 
         */
        private void RadarVisualisation(int deg, float dist)
        {
            // Convert deg to range [-90,90]
            handDegMain = -(deg - 90);
            // Convert dist relative to HAND & maskedDistance
            maskedDistance = float.Parse(maskedDistanceTextBox.Text);
            float adjustedDist = dist > maskedDistance ?
                                        HAND : (dist * HAND) / maskedDistance;

       
            if (handDegMain >= 0 && handDegMain <= 90)
            {
                // right half
                // degree is converted into radian
                handGreenX = circleX + (int)(HAND * Math.Sin(Math.PI * handDegMain / 180));
                handGreenY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegMain / 180));
                // Red Hand
                handRedX = circleX + (int)(adjustedDist * Math.Sin(Math.PI * handDegMain / 180));
                handRedY = circleY - (int)(adjustedDist * Math.Cos(Math.PI * handDegMain / 180));
            }
            else
            {
                // left half
                handGreenX = circleX - (int)(HAND * -Math.Sin(Math.PI * handDegMain / 180));
                handGreenY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegMain / 180));
                // Red Hand
                handRedX = circleX - (int)(adjustedDist * -Math.Sin(Math.PI * handDegMain / 180));
                handRedY = circleY - (int)(adjustedDist * Math.Cos(Math.PI * handDegMain / 180));
            }

            /////////////////////////////
            //      Draw Radar
            ////////////////////////////
            //graphics
            graphics = Graphics.FromImage(bmp);

            // draw circles
            for (int i = 50; i < HAND; i += 100)
            {
                int radius = HAND - i;
                graphics.DrawEllipse(pen, circleX - radius, circleY - radius, radius * 2, radius * 2);
            }

            //draw perpendicular line
            graphics.DrawLine(pen, new Point(circleX, circleY), new Point(circleX, HEIGHT)); // UP-DOWN
            // draw angled lines
            for (int ang = 30; ang < MAX_SERVO_ANGLE; ang += 30)
            {
                Point p_end = new Point(circleX - (int)(HAND * Math.Cos(ang * Math.PI / 180)), circleY - (int)(HAND * Math.Sin(ang * Math.PI / 180)));
                graphics.DrawLine(pen, new Point(circleX, circleY), p_end);
            }
            //////////////////
            //   Draw HAND
            //////////////////

            Point start_p  = new Point(circleX, circleY);
            Point middle_p = new Point(handGreenX, handGreenY);
            Point end_p    = new Point(handRedX, handRedY);

            // Shift Lines
            for (int i = linesArray.Length - 1 ; i > 0 ; i -= 3)
            {
                if (linesArray[i].IsEmpty)
                    break;
                linesArray[i] = linesArray[i - 3];
                linesArray[i - 1] = linesArray[i - 4];
                linesArray[i - 2] = linesArray[i - 5];
            }
            // insert new line
            linesArray[0] = start_p;
            linesArray[1] = middle_p;
            linesArray[2] = end_p;

            // Draw Lines
            for (int i = 0; i < linesArray.Length; i += 3)
            {
                if (linesArray[i].IsEmpty)
                    break;
                graphics.DrawLine(penGreenList[i/3], linesArray[i], linesArray[i+1]);
                graphics.DrawLine(penRedList[i/3], linesArray[i+1], linesArray[i+2]);
            }

            graphics.Dispose();
            //load bitmap in picturebox1
            radarPictureBox.Image = bmp;

        } // END RadarVisualisation

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
         * Listens to UART interrupts
         */
        private void DataRecievedHandler(Object sender, SerialDataReceivedEventArgs e)
        {
            CustomSerialPort sp = (CustomSerialPort)sender;
            try 
            {
                inData = sp.readMessage();

            }
            catch (Exception)
            {
                serialPort.Close();
                updateRadarStatusLabel(CustomSerialPort.STATUS.ToString(CustomSerialPort.STATUS.PORT_ERROR));
                this.Invoke((MethodInvoker)delegate
                {
                    setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
                });


            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            handleMessage(inData.Item1, inData.Item2, inData.Item3);

        }


        /// <summary>
        /// Handle Message
        /// </summary>
        /// <param name="opc">operation code</param>
        /// <param name="val">value</param>
        /// <param name="checksumStatus">check sum</param>
        private void handleMessage(string opc, string val, int checksumStatus)
        {
            // Checksum check
            if (checksumStatus == CustomSerialPort.STATUS.CHECKSUM_ERROR)
            {
                updateRadarStatusLabel(CustomSerialPort.STATUS.ToString(CustomSerialPort.STATUS.CHECKSUM_ERROR));
                Console.WriteLine(CustomSerialPort.STATUS.ToString(CustomSerialPort.STATUS.CHECKSUM_ERROR));
                return;
            }
            
            // Check opc
            switch (opc)
            {
                // Baud rate change acknoledge
                case CustomSerialPort.TYPE.BAUDRATE:
                    try
                    {
                        serialPort.Close();
                        Thread.Sleep(CustomSerialPort.CONFIGURE_DELAY);
                        serialPort.BaudRate = int.Parse(val);
                        serialPort.Open();
                        this.Invoke((MethodInvoker)delegate
                        {
                            setConnectingLabel(CustomSerialPort.STATUS.PORT_OK);
                        });
                        
                    }
                    catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }
                    break;

                // Recieve Scanner info
                case CustomSerialPort.TYPE.SCAN:
                    int deg = int.Parse(val.Substring(0, 3));
                    int tpmDiff = int.Parse(val.Substring(3));
                    float dist = calcDistsnce(tpmDiff);
                    Console.WriteLine("scan: deg- " + deg + " dist- " + dist + " cm");
                    this.Invoke((MethodInvoker)delegate
                    {
                        if(!scanMode)
                            scanButton_Click(this,EventArgs.Empty);
                        RadarVisualisation(deg, dist);
                        angleLabel.Text = "Angle: " + deg;
                        distanceLabel.Text = "Distance: " + dist.ToString("#.##");
                    });
                    break;

                // Recieve Telemetria info
                case CustomSerialPort.TYPE.TELMETERIA:
                    Console.WriteLine("Telemetria: Distance- " + float.Parse(val) + "cm" );
                    this.Invoke((MethodInvoker)delegate
                    {
                        telemetriaDistanceLabel.Text = "Distance: " + calcDistsnce(float.Parse(val));
                        telemetriaCmLabel.Visible = true;
                    });
                    break;

                // Get MCU Serial Port Status
                case CustomSerialPort.TYPE.STATUS:
                    handleStatusMessage(int.Parse(val));
                    break;

                // Recieving file
                case CustomSerialPort.TYPE.FILE_START:
                    selectTab(1);
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_OK)
                    {
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_FILE;
                        Console.WriteLine("======================================================");
                        Console.WriteLine("recieving a file...");
                        updateFileTransferStatusLabel("recieving a file...");

                    }
                    else {
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_ERROR;
                        CustomSerialPort.RFile.Status = CustomSerialPort.STATUS.RECIEVING_OK;
                        Console.WriteLine("ERROR - trying to send a new file when already recieving a file");
                        updateFileTransferStatusLabel("ERROR - trying to send a new file when already recieving a file");
                    }
                    break;

                // Recieving file's Name
                case CustomSerialPort.TYPE.FILE_NAME:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Name = val;
                    Console.WriteLine("recieving \"" + val + "\"");
                    updateFileTransferStatusLabel("recieving \"" + val + "\"");


                    break;

                // Recieving file's Size
                case CustomSerialPort.TYPE.FILE_SIZE:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Size = int.Parse(val);
                    Console.WriteLine("file size: " + val);
                    updateFileTransferStatusLabel("file size: " + val);
                    
                    break;

                // Recieving file's Data
                case CustomSerialPort.TYPE.FILE_DATA:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("writing file data ERROR");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        break;
                    }
                    CustomSerialPort.RFile.Data += val;                       
                    Console.WriteLine(val);
                    updateFileTransferStatusLabel("received " + CustomSerialPort.RFile.Data.Length + " / " + CustomSerialPort.RFile.Size + " bytes");

                    if(CustomSerialPort.RFile.Data.Length == CustomSerialPort.RFile.Size)
                    {
                        string path = Path.Combine(currentDirectoryPath, filesRecievedDirectory, CustomSerialPort.RFile.Name);
                        File.WriteAllText(path, CustomSerialPort.RFile.Data);
                        Console.WriteLine("\"" + CustomSerialPort.RFile.Name + "\" received successfully");
                        Console.WriteLine("======================================================");
                        updateFileTransferStatusLabel("\"" + CustomSerialPort.RFile.Name + "\" received successfully");
                        CustomSerialPort.RFile.clean();
                    }
                    break;

                // File recieved ok 
                case CustomSerialPort.TYPE.FILE_END:
                    if (int.Parse(val) == CustomSerialPort.STATUS.OK)
                    {
                        updateFileTransferStatusLabel("\"" + Path.GetFileName(selectedFilePath) + "\" sent successfully");
                        Console.WriteLine("\"" + Path.GetFileName(selectedFilePath) + "\" sent successfully");
                        Console.WriteLine("======================================================");
                    }
                    else
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("\"" + CustomSerialPort.RFile.Name + "\" did not send successfully");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        updateFileTransferStatusLabel("\"" + CustomSerialPort.RFile.Name + "\" did not send successfully");
                    }
                    break;

                // Unknown
                default:
                    Console.WriteLine("unreccognized type");
                    updateFileTransferStatusLabel("unreccognized type");
                    break;


            }
       
        } // END handleMessage

        /*
         * Calculates distance from TPM register diff
         */ 
        private float calcDistsnce(float tpmDiff)
        {
            // time diff: 1 / (24M/8)  | 24MHz = tpm clk , 8 = tpm prescaler

            // float tpmTimeDiff = (float)tpmDiff / (float) 3000000; 
            // float dist = tpmTimeDiff * 17000;
            return (float)tpmDiff / (float)176.4705882;
        } 

        /*
         * Handle Status Type Message
         */
        private void handleStatusMessage(int status)
        {
            switch (status)
            {
                case CustomSerialPort.STATUS.RECIEVING_MESSAGE:
                    updateRadarStatusLabel(CustomSerialPort.STATUS.ToString(status));
                    break;

                case CustomSerialPort.STATUS.BUFFER_ERROR:
                    updateRadarStatusLabel(CustomSerialPort.STATUS.ToString(status));
                    CustomSerialPort.STATUS.bufferErrorCounter++;
                    if(CustomSerialPort.STATUS.bufferErrorCounter > CustomSerialPort.STATUS.BUFFER_ERROR_OVERFLOW)
                    {
                        CustomSerialPort.STATUS.bufferErrorCounter = 0;
                        this.Invoke((MethodInvoker)delegate
                        {
                            setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
                        });
                    }
                    break;

                case CustomSerialPort.STATUS.OK:
                    updateRadarStatusLabel(CustomSerialPort.STATUS.ToString(status));
                    break;
                default:
                    // TODO: maybe UI change
                    break;
            }

        }

        /*
        * Dispaly connectingt label
        */
        private void setConnectingLabel(int status)
        {
            Brush brush = Brushes.Green;
            switch (status)
            {
                case CustomSerialPort.STATUS.PORT_OK:
                case CustomSerialPort.STATUS.OK:
                    brush = Brushes.Green;
                    this.connectingLabel.Text = "Connected to " + serialPort.PortName + " with Baudrate " + serialPort.BaudRate + " BPS";
                    break;

                case CustomSerialPort.STATUS.PORT_ERROR:
                    brush = Brushes.Red;
                    this.connectingLabel.Text = "Configure Serial Port";
                    break;

            }
            int nSize = 8;
            int x = (panel1.Size.Width - connectingLabel.Size.Width) / 2;
            int y = (panel1.Size.Height - connectingLabel.Size.Height) / 2; ;
            connectingLabel.Location = new Point(x, y);
            connectingPictureBox.Location = new Point(connectingLabel.Location.X - 13, connectingLabel.Location.Y + 3);
            Bitmap bm = new Bitmap(connectingPictureBox.Width, connectingPictureBox.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.FillEllipse(brush, Convert.ToInt32((connectingPictureBox.Width - nSize) / 2), Convert.ToInt32((connectingPictureBox.Height - nSize) / 2), nSize, nSize);
            }

            connectingPictureBox.Image = bm;
        }

        /*
         * Set Configuretions
         */
        private void configurationButton_click(object sender, EventArgs e)
        {
            // start configuration window
            if ((Application.OpenForms["ConfigurationsForm"] as ConfigurationsForm) != null)
            {
                //Form is already open
                Application.OpenForms["ConfigurationsForm"].Close();
            }

            ConfigurationsForm configurationsForm = new ConfigurationsForm(ref serialPort);
            configurationsForm.MinimizeBox = false;
            configurationsForm.MaximizeBox = false;
            configurationsForm.Show();
        }

        /*
        * Listen to EnterKey to send data
        */
        private void mainForm_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
                    scanButton_Click(sender, new EventArgs());
                else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                    sendFileButton_Click(sender, new EventArgs());
            }
        }

        /*
        * Serial Button Mouse Hover
        */
        private void settingsPanel_MouseHover(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.LightSeaGreen;
        }

        /*
         * Serial Button Mouse Leave
         */
        private void settingsPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.settingsPanel.BackColor = Color.Transparent;
        }

       /*
        * Serial Confighrations Change Listener
        */
        private void onSerialConfigurationsChanged(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                setConnectingLabel(CustomSerialPort.STATUS.OK);
            });
        }

        /*
         * File Sending Progress updater
         */ 
        private void onFileSendingProgress(object sender, EventArgs e)
        {
            updateFileTransferStatusLabel(sender.ToString());
        }

        /*
         * Select Tab
         */
        private void selectTab(int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    tabControl1.SelectTab(index);
                });
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                Radar Tab
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /*
         * Update Radar Status labels
         */
        private void updateRadarStatusLabel(string statusLabelString)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = statusLabelString;
                });
            }
            else
                statusLabel.Text = statusLabelString;
        }

        /*
         * On Send Message Button Click 
         */
        private void telemetriaButton_Click(object sender, EventArgs e)
        {
            if (telemetriaDataTextBox.Text.Equals(""))
                return;

            // Send Message to MCU
            try
            {
                int deg = int.Parse(telemetriaDataTextBox.Text);
                if(deg > MAX_SERVO_ANGLE || deg < 0 )
                {
                    updateRadarStatusLabel("servo degree range error");
                    return;
                }
                serialPort.sendMessage(CustomSerialPort.TYPE.TELMETERIA, telemetriaDataTextBox.Text);
                telemetriaDataTextBox.Text = "";
                updateRadarStatusLabel("");
            }
            catch (Exception)
            {
                updateRadarStatusLabel("sevo degree accepts only integers in range [0,180]");
            }
        }

        /*
         * Scan Radar Button Click Listener
         */ 
        private void scanButton_Click(object sender, EventArgs e)
        {
            if (scanMode) // Stop scanning
            {
                scanMode = false;
                serialPort.sendMessage(CustomSerialPort.TYPE.STOP_RADAR, "");
                scanButton.Text = "Start Scan";
                radarPictureBox.Visible = false;
                radarTextPanel.Visible = false;
                deg30Label.Visible = false;
                deg60Label.Visible = false;
                deg90Label.Visible = false;
                deg120Label.Visible = false;
                deg150Label.Visible = false;
                // Telemetria On
                maskedDistancePanel.Visible = true;
                telemetriaButton.Visible = true;
                telemetriaDataTextBox.Visible = true;
                telemetriaLabel.Visible = true;
                telemetriaPanel.Visible = true;
                radarPanel.BackColor = Color.Transparent;
                t.Stop();
            }
            else // start scanning
            {
                scanMode = true;
                serialPort.sendMessage(CustomSerialPort.TYPE.SCAN,"");
                maskedDistance = float.Parse(maskedDistanceTextBox.Text);
                scanButton.Text = "Stop Scan";
                radarPictureBox.Visible = true;
                radarTextPanel.Visible = true;
                deg30Label.Visible = true;
                deg60Label.Visible = true;
                deg90Label.Visible = true;
                deg120Label.Visible = true;
                deg150Label.Visible = true;
                // Telemetria Off
                maskedDistancePanel.Visible = false;
                telemetriaButton.Visible = false;
                telemetriaDataTextBox.Visible = false;
                telemetriaLabel.Visible = false;
                telemetriaPanel.Visible = false;
                radarPanel.BackColor = Color.Black;
                t.Start();
            }
           
        }

        /*
         * Delete Script Button Click
         */ 
        private void deleteScriptButton_Click(object sender, EventArgs e)
        {
            if (selectedFilePath.Equals(""))
                return;
            Console.WriteLine(Path.GetFileName(selectedFilePath) + " Deleted");
            File.Delete(selectedFilePath);
            filesListView.Clear();

        }

        /*
         * Save Script Button Click
         */ 
        private void saveScriptButton_Click(object sender, EventArgs e)
        {
            if (scriptNameTextBox.Text.Equals("") || scriptDataRichTextBox.Text.Equals(""))
                return;
            string path = Path.Combine(currentDirectoryPath, filesToSendDirectory, scriptNameTextBox.Text + ".txt");
            File.WriteAllText(path, scriptDataRichTextBox.Text);
            filesListView.Clear();
            initializeFilesListView();
            scriptNameTextBox.Text = "";
            scriptDataRichTextBox.Text = "";
        }


        /*
         * NFB Button click to send data using Write method
         */
        private void nonFormatButon_Click(object sender, EventArgs e)
        {
            serialPort.Write(telemetriaDataTextBox.Text);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //          Files Tab
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

       /*
        * Updates File Transfer UI Labels
        */
        public void updateFileTransferStatusLabel(string fileStatusLabelString)
        {
            this.Invoke((MethodInvoker)delegate
            {
                fileStatusLabel.Text = fileStatusLabelString;
            });
        }

        public void outsideUpdateFileTransferUI(string fileStatusLabelString)
        {
            fileStatusLabel.Invoke(new MethodInvoker(() => fileStatusLabel.Text = fileStatusLabelString));
        }

        /*
         * On List View Item Click
         */
        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems.Count >= 1)
            {
                string selectedFile = filesListView.SelectedItems[0].Text;
                string[] fileEntries = Directory.GetFiles(dataFilesPath);
                foreach (string filePath in fileEntries)
                {
                    if (Path.GetFileName(filePath).Equals(selectedFile) && Path.GetExtension(filePath) == ".txt")
                    {
                        selectedFilePath = filePath;
                    }
                }

            }
        }

        /*
         * Initialize Files Text View with files 
         */
        private void initializeFilesListView()
        {
            this.filesListView.Scrollable = true;
            // Set Column
            this.filesListView.Columns.Add("Name", 300, HorizontalAlignment.Left);
            this.filesListView.Columns.Add("Size", -2, HorizontalAlignment.Left);
            // get Directory
            dataFilesPath = Path.Combine(currentDirectoryPath, filesToSendDirectory);

            if (Directory.Exists(dataFilesPath))
            {
                // Process the list of files found in the directory
                ListViewItem listViewItem;
                string[] fileEntries = Directory.GetFiles(dataFilesPath);
                foreach (string filePath in fileEntries)
                {
                    FileInfo file = new FileInfo(filePath);
                    if (file.Extension.Equals(".txt"))
                    {
                        string[] row = { file.Name, file.Length.ToString() + " Bytes" };
                        listViewItem = new ListViewItem(row);
                        filesListView.Items.Add(listViewItem);
                    }
                }
            }
           
            filesListView.Focus();
        }

        /*
         * Send File Button Click
         */ 
        private void sendFileButton_Click(object sender, EventArgs e)
        {
            if (selectedFilePath.Equals(""))
                return;
           

            // Send File to MCU
            try
            {
                CustomSerialPort.RFile.clean();
                fileStatusLabel.Text = "sending \"" + Path.GetFileName(selectedFilePath) + "\"";
                Console.WriteLine("========================================================");
                Console.WriteLine("sending \"" + Path.GetFileName(selectedFilePath) + "\"");
                serialPort.sendFile(selectedFilePath);
            }
            catch (Exception exception) {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("error sending \"" + Path.GetFileName(selectedFilePath) + "\"" + exception.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                fileStatusLabel.Text = "error sending \"" + Path.GetFileName(selectedFilePath) + "\"";
                setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
            }
        }

        /*
         * Prevent column resize
         */ 
        private void filesListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = filesListView.Columns[e.ColumnIndex].Width;
        }

        /*
         * Get Color From HSV 
         */ 
        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
