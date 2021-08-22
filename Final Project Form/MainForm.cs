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

        // For Files
        private string currentDirectoryPath;
        private string filesToSendDirectory = "Final Project Files/Files to send";
        private string filesRecievedDirectory = "Final Project Files/Files recieved";
        private string selectedFilePath = "";
        private string dataFilesPath = "";

        // Radar System
        private bool scanMode = false;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        int WIDTH, HEIGHT, HAND;

        private int handDegMain, handDegBlack;                       // in degree
        private int handMainX, handMainY;                            // HAND coordinate
        private const int LIM = 15, MAX_SERVO_ANGLE = 180;
        private int handBlackX, handBlackY;                          // black & green hand
        private int[] handGreenX = new int[LIM], handGreenY = new int[LIM], handDegGreen = new int[LIM];
        private int circleX, circleY;                                // center of the circle
        private int handStartAngle = -90;

        private bool left_to_right = true;
        // radar drawing
        private Bitmap bmp;
        private Pen pen;
        private Pen[] penGreen = new Pen[LIM];
        private Graphics graphics;
        private static double hue = Color.Green.GetHue();
        private static double saturation = Color.Green.GetSaturation();
        private static double brightness = Color.Green.GetBrightness();
        private Color[] colorList = new Color[LIM];


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

            for (int i = 0; i < LIM; i++)
                colorList[i] = ColorFromHSV(hue, saturation, brightness + (1- brightness)/LIM * i);

            //timer
            t.Interval = 20; //in millisecond
            t.Tick += new EventHandler(this.t_Tick);

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////           Main Methods
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

       /*
        * Tick Tock Radar 
        */ 
        private void t_Tick(object sender, EventArgs e)
        {
            //pen
            pen = new Pen(Color.Green, 1f);
            for (int i = 0; i < LIM; i++)
                penGreen[i] =  new Pen(colorList[i], 1f);
            

            //graphics
            graphics = Graphics.FromImage(bmp);

            angleLabel.Text = "Angle: " + (handDegMain + 90);

            //calculate x, y coordinate of HAND
            if (left_to_right)
            {
                handDegBlack = handDegMain - LIM;
                for(int i = LIM-1 ; i > 0 ; i--)
                    handDegGreen[i] = handDegMain - LIM + i+1;
            }
            else
            {
                handDegBlack = handDegMain + LIM;
                for (int i = LIM - 1; i > 0; i--)
                    handDegGreen[i] = handDegMain + LIM - i-1;
            }

            // swipe hand back and forth
            if (handDegMain == 90)
                left_to_right = false;
            else if(handDegMain == -90)
                left_to_right = true;

            // MAIN HAND
            if (handDegMain >= 0 && handDegMain <= MAX_SERVO_ANGLE)
            {
                // right half
                // u in degree is converted into radian.

                handMainX = circleX + (int)(HAND * Math.Sin(Math.PI * handDegMain / 180));
                handMainY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegMain / 180));
            }
            else
            {
                // left half
                handMainX = circleX - (int)(HAND * -Math.Sin(Math.PI * handDegMain / 180));
                handMainY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegMain / 180));
            }
            // BLACK HAND
            if (handDegBlack >= 0 && handDegBlack <= MAX_SERVO_ANGLE)
            {
                //right half
                //degree is converted into radian.

                handBlackX = circleX + (int)(HAND * Math.Sin(Math.PI * handDegBlack / 180));
                handBlackY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegBlack / 180));
            }
            else
            {
                handBlackX = circleX - (int)(HAND * -Math.Sin(Math.PI * handDegBlack / 180));
                handBlackY = circleY - (int)(HAND * Math.Cos(Math.PI * handDegBlack / 180));
            }

            // GREEN 2 HAND
            for (int i = 0; i < LIM; i++)
            {
                if (handDegGreen[i] >= 0 && handDegGreen[i] <= MAX_SERVO_ANGLE)
                {
                    //right half
                    //degree is converted into radian.

                    handGreenX[i] = circleX + (int)(HAND * Math.Sin(Math.PI * handDegGreen[i] / 180));
                    handGreenY[i] = circleY - (int)(HAND * Math.Cos(Math.PI * handDegGreen[i] / 180));

                }
                else
                {

                    handGreenX[i] = circleX - (int)(HAND * -Math.Sin(Math.PI * handDegGreen[i] / 180));
                    handGreenY[i] = circleY - (int)(HAND * Math.Cos(Math.PI * handDegGreen[i] / 180));

                }
            }

            //////////////////////////////
            //       Draw Radar 
            /////////////////////////////

            // draw circles
            for (int i = 50 ; i < HAND ; i += 100)
            {
                int radius = HAND - i;
                graphics.DrawEllipse(pen, circleX - radius, circleY - radius, radius * 2, radius * 2);  
            }

            //draw perpendicular line
            graphics.DrawLine(pen, new Point(circleX, 0), new Point(circleX, HEIGHT)); // UP-DOWN
            // draw angled lines
            for (int ang = 30; ang < MAX_SERVO_ANGLE; ang += 30)
            {
                Point p_end = new Point(circleX - (int)(HAND * Math.Cos(ang * Math.PI / 180)), circleY - (int)(HAND * Math.Sin(ang * Math.PI / 180)));
                graphics.DrawLine(pen, new Point(circleX, HEIGHT), p_end);
            }

            //draw HAND
            graphics.DrawLine(pen, new Point(circleX, circleY), new Point(handMainX, handMainY));
            for (int i = 0; i < LIM; i++)
            {
                graphics.DrawLine(penGreen[i], new Point(circleX, circleY), new Point(handGreenX[i], handGreenY[i]));
                penGreen[i].Dispose();
            }
            graphics.DrawLine(new Pen(Color.Black, 1f), new Point(circleX, circleY), new Point(handBlackX, handBlackY));

            //load bitmap in picturebox1
            radarPictureBox.Image = bmp;

            //dispose
            pen.Dispose();
            graphics.Dispose();

            //update
            if(left_to_right)
                handDegMain++;
            else
                handDegMain--;

            if (handDegMain == MAX_SERVO_ANGLE)
            {
                handDegMain = handStartAngle;
            }
        } // ens tick-tock radar

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
                case CustomSerialPort.Type.BAUDRATE:
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
                case CustomSerialPort.Type.SCAN:
                    int deg = int.Parse(val.Substring(0, 3));
                    float dist = float.Parse(val.Substring(3));
                    Console.WriteLine("scan: deg- " + deg + " dist- " + dist + " cm");
                    this.Invoke((MethodInvoker)delegate
                    {
                        angleLabel.Text = deg.ToString();
                        distanceLabel.Text = dist.ToString();
                    });
                    break;

                // Recieve Telemetria info
                case CustomSerialPort.Type.TELMETERIA:
                    Console.WriteLine("Telemetria: Distance- " + val);
                    this.Invoke((MethodInvoker)delegate
                    {
                        telemetriaDistanceLabel.Text = val.ToString();
                    });
                    break;

                // Get MCU Serial Port Status
                case CustomSerialPort.Type.STATUS:
                    handleStatusMessage(int.Parse(val));
                    break;

                // Recieving file
                case CustomSerialPort.Type.FILE_START:
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
                case CustomSerialPort.Type.FILE_NAME:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Name = val;
                    Console.WriteLine("recieving \"" + val + "\"");
                    updateFileTransferStatusLabel("recieving \"" + val + "\"");


                    break;

                // Recieving file's Size
                case CustomSerialPort.Type.FILE_SIZE:
                    if (CustomSerialPort.RFile.Status == CustomSerialPort.STATUS.RECIEVING_ERROR)
                        break;
                    CustomSerialPort.RFile.Size = int.Parse(val);
                    Console.WriteLine("file size: " + val);
                    updateFileTransferStatusLabel("file size: " + val);
                    
                    break;

                // Recieving file's Data
                case CustomSerialPort.Type.FILE_DATA:
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
                case CustomSerialPort.Type.FILE_END:
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
                serialPort.sendMessage(CustomSerialPort.Type.TEXT, telemetriaDataTextBox.Text);
                telemetriaDataTextBox.Text = "";
            }
            catch (Exception)
            {
                setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR);
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
                scanButton.Text = "Start Scan";
                radarPictureBox.Visible = false;
                radarTextPanel.Visible = false;
                deg30Label.Visible = false;
                deg60Label.Visible = false;
                deg90Label.Visible = false;
                deg120Label.Visible = false;
                deg150Label.Visible = false;
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
                scanButton.Text = "Stop Scan";
                radarPictureBox.Visible = true;
                radarTextPanel.Visible = true;
                deg30Label.Visible = true;
                deg60Label.Visible = true;
                deg90Label.Visible = true;
                deg120Label.Visible = true;
                deg150Label.Visible = true;
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
