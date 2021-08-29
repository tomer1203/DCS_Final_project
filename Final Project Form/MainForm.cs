using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FinalProject.Source_files;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace FinalProject
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
        private string selectedFilePath = "";
        private string dataFilesPath = "";

        ////////////////
        // Radar System
        ////////////////
        private bool scanMode = false;
        private float maskedDistance =  100; // cm

        private int WIDTH, HEIGHT, HAND;
        private int handDegMain, beamRange = 120;                                     // in degree
        private int handGreenX, handGreenY, handRedX, handRedY;      // HAND coordinate
        private const int MAX_SERVO_ANGLE = 180;
        private int circleX, circleY;                                // center of the circle
        private Point[] linesArray;

        // radar drawing
        private Bitmap bmp;
        private Pen pen = new Pen(Color.Green, 2.5f);
        private Pen[] penGreenList;
        private Pen[] penRedList;
        private Graphics graphics;
        private static double greenHue = Color.Green.GetHue();
        private static double greenSaturation = Color.Green.GetSaturation();
        private static double greenBrightness = Color.Green.GetBrightness();
        private Color[] greenColorList;
        private static double redHue = Color.Red.GetHue();
        private static double redSaturation = Color.Red.GetSaturation();
        private static double redBrightness = Color.Red.GetBrightness();
        private Color[] redColorList;


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
            // Get Files Directory Path
            dataFilesPath = Path.Combine(currentDirectoryPath, filesToSendDirectory);

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
            EventHub.SaveSerialConfigurationsHandler += onSerialConfigurationsChanged;
            EventHub.SaveRadarConfigurationsHandler += onRadarConfigurationsChanged;
            EventHub.SaveFileConfigurationsHandler += onFilesConfigurationsChanged;
            EventHub.FileSendingProgressHandler += onFileSendingProgress;
           

            onRadarConfigurationsChanged(this, (maskedDistance, beamRange));

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
            float adjustedDist = dist > maskedDistance ?
                                        HAND : (dist * HAND) / maskedDistance;

            // Initiallize Pens
            penGreenList = new Pen[beamRange];
            penRedList = new Pen[beamRange];
            for (int i = 0; i < beamRange; i++)
            {
                penGreenList[i] = new Pen(greenColorList[i], 2.5f);
                penRedList[i] = new Pen(redColorList[i], 2.5f);
            }


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
            Point temp0 ,temp1,temp2;

            for (int i = 0 ; i < linesArray.Length - 6; i += 6)
            {
                if (linesArray[i].IsEmpty && linesArray[i+3].IsEmpty)
                    break;

                temp0 = linesArray[i + 3];
                temp1 = linesArray[i + 4];
                temp2 = linesArray[i + 5];
                linesArray[i + 3] = linesArray[i];
                linesArray[i + 4] = linesArray[i + 1];
                linesArray[i + 5] = linesArray[i + 2];
                linesArray[i + 6] = temp0;
                linesArray[i + 7] = temp1;
                linesArray[i + 8] = temp2;

            }
            // insert new line
            linesArray[0] = start_p;
            linesArray[1] = middle_p;
            linesArray[2] = end_p;

            // Draw Lines
            for (int i = 0 ; i < linesArray.Length ; i += 3)
            {
                if (linesArray[i].IsEmpty)
                    break;
                graphics.DrawLine(penGreenList[i/3], linesArray[i], linesArray[i+1]);
                graphics.DrawLine(penRedList[i/3], linesArray[i+1], linesArray[i+2]);
                penGreenList[i / 3].Dispose();
                penRedList[i / 3].Dispose();
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
                    Console.WriteLine("ERROR - DataRecievedHandler - Closing Port");
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
                            scanButton_Click("false",EventArgs.Empty);
                        RadarVisualisation(deg, dist);
                        angleLabel.Text = "Angle: " + deg;
                        distanceLabel.Text = "Distance: " + dist.ToString("#.##");
                    });
                    break;

                // Recieve Telemetry info
                case CustomSerialPort.TYPE.TELMETERIA:
                    string distanceString = calcDistsnce(int.Parse(val)) > maskedDistance ?
                        "Out of Range" : calcDistsnce(int.Parse(val)).ToString("#.##") + " cm";
                    Console.WriteLine("Telemetria: Distance- " + distanceString);
                    updateFileTransferStatusLabel("");
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (scanMode)
                            scanButton_Click("false", EventArgs.Empty);
                        stopTelemetriaButton.Visible = true;
                        scanButton.Visible = false;
                        telemetriaDistanceLabel.Text = "Distance: " + distanceString;
                        telemetriaPanel.Visible = true;
                    });
                    break;

                // Get MCU Serial Port Status
                case CustomSerialPort.TYPE.STATUS:
                    handleStatusMessage(int.Parse(val));
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
                        // STOP SENDING FILE
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("\"" + CustomSerialPort.RFile.Name + "\" did not send successfully with status " );
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
        private float calcDistsnce(int tpmDiff)
        {
            // final clk: 1 / (tpm clk/prescaler)  | tpm clk = 24MHz  , tpm prescaler = 32 
            // => final clk = 24M/32 = 750,000 Hz
            // dist = tpmDiff * (17,150 / final clk); 
            // prescaler = 8 : 176.4705882352941
            // prescaler = 32 : 43.731778425655976676384839650146
            //return (float)(tpmDiff * 0.022866666666);
            return (float)tpmDiff / (float)43.731778425655976676384839650146;
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
                            Console.WriteLine("Buffer Error");
                            setConnectingLabel(CustomSerialPort.STATUS.BUFFER_ERROR);
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

                    case CustomSerialPort.STATUS.BUFFER_ERROR:
                    brush = Brushes.Orange;
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
         * Set Serial Configuretions
         */
        private void SerialConfigurationButton_Click(object sender, EventArgs e)
        {
            // start configuration window
            if ((Application.OpenForms["ConfigurationsForm"] as SerialConfigurationsForm) != null)
            {
                //Form is already open
                Application.OpenForms["ConfigurationsForm"].Close();
            }

            SerialConfigurationsForm configurationsForm = new SerialConfigurationsForm(ref serialPort);
            configurationsForm.MinimizeBox = false;
            configurationsForm.MaximizeBox = false;
            configurationsForm.Show();
        }

        /*
        * Set Files Configuretions
        */
        private void FilesConfigurationsButton_Click(object sender, EventArgs e)
        {
            // start configuration window
            if ((Application.OpenForms["filesConfigurationsForm"] as FilesConfigurationsForm) != null)
            {
                //Form is already open
                Application.OpenForms["filesConfigurationsForm"].Close();
            }

            
            FilesConfigurationsForm filesConfigurationsForm = new FilesConfigurationsForm(Path.Combine(currentDirectoryPath, filesToSendDirectory));
            filesConfigurationsForm.MinimizeBox = false;
            filesConfigurationsForm.MaximizeBox = false;
            filesConfigurationsForm.Show();
        }

        /*
        * Files Confighrations Change Listener
        */
        private void onFilesConfigurationsChanged(object sender, EventArgs e)
        {
            this.dataFilesPath = sender.ToString();
            filesListView.Clear();
            initializeFilesListView();
        }

        /*
       * Set Radar Configuretions
       */
        private void RadarConfigurationsButton_Click(object sender, EventArgs e)
        {
            // start configuration window
            if ((Application.OpenForms["radarConfigurationsForm"] as RadarConfigurationsForm) != null)
            {
                //Form is already open
                Application.OpenForms["radarConfigurationsForm"].Close();
            }


            RadarConfigurationsForm radarConfigurationsForm = new RadarConfigurationsForm(maskedDistance, beamRange);
            radarConfigurationsForm.MinimizeBox = false;
            radarConfigurationsForm.MaximizeBox = false;
            radarConfigurationsForm.Show();
        }

        /*
        * Radar Confighrations Change Listener
        */
        private void onRadarConfigurationsChanged(object sender, (float maskedDistance, int beamRange) e)
        {           

            this.maskedDistance = e.maskedDistance;
            this.beamRange = e.beamRange * 2;

            MDLabel.Text = "M.D.:" + maskedDistance;

            // Erase Lines
            EraseLines();

            linesArray = new Point[3 * beamRange];
            redColorList = new Color[beamRange];
            greenColorList = new Color[beamRange];

            for (int i = 0; i < beamRange - 1; i++)
            {
                greenColorList[i] = ColorFromHSV(greenHue, greenSaturation, greenBrightness - (greenBrightness * i / (beamRange - 1)));
                redColorList[i] = ColorFromHSV(redHue, redSaturation, redBrightness - (redBrightness * i / (beamRange - 1)));
            }
            greenColorList[0] = Color.Green;
            greenColorList[beamRange - 1] = Color.Black;
            redColorList[0] = Color.Red;
            redColorList[beamRange - 1] = Color.Black;
        }

        private void EraseLines()
        {
            try
            {
                graphics = Graphics.FromImage(bmp);
                Pen blackPen = new Pen(Color.Black, 2.5f);
                for (int i = 0; i < linesArray.Length; i += 3)
                {
                    if (linesArray[i].IsEmpty)
                        break;
                    graphics.DrawLine(blackPen, linesArray[i], linesArray[i + 1]);
                    graphics.DrawLine(blackPen, linesArray[i + 1], linesArray[i + 2]);
                }
                blackPen.Dispose();
                graphics.Dispose();
                radarPictureBox.Image = bmp;
                Array.Clear(linesArray, 0, linesArray.Length);
                

            }
            catch (Exception) { Console.WriteLine("error erasing lines"); }
        }

        /*
        * Listen to EnterKey to send data
        */
        private void mainForm_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                if (telemetriaDataTextBox.Focused) telemetriaButton_Click(sender, new EventArgs());
                else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
                    scanButton_Click("true", new EventArgs());
                else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                    sendFileButton_Click(sender, new EventArgs());
            }
        }

        /*
         * Radar Configuration Button Mouse Hover
         */
        private void radarConfigurationPanel_MouseHover(Object sender, EventArgs e)
        {
            this.radarConfigurationPanel.BackColor = Color.LightSeaGreen;
        }

        /*
         * Radar Configuration Button Mouse Leave
         */
        private void radarConfigurationPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.radarConfigurationPanel.BackColor = Color.Transparent;
        }

        /*
         * Files Configuration Button Mouse Hover
         */
        private void filesConfigurationPanel_MouseHover(Object sender, EventArgs e)
        {
            this.filesConfigurationPanel.BackColor = Color.LightSeaGreen;
        }

        /*
         * Files Configuration Button Mouse Leave
         */
        private void filesConfigurationPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.filesConfigurationPanel.BackColor = Color.Transparent;
        }

        /*
         * Serial Button Mouse Hover
         */
        private void settingsPanel_MouseHover(Object sender, EventArgs e)
        {
            this.serialConfigurationPanel.BackColor = Color.LightSeaGreen;
        }


        /*
         * Serial Button Mouse Leave
         */
        private void settingsPanel_MouseLeave(Object sender, EventArgs e)
        {
            this.serialConfigurationPanel.BackColor = Color.Transparent;
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
            {
                updateRadarStatusLabel("enter servo angle");
                return;
            }
                

            // Send Message to MCU
            try
            {
                int deg = int.Parse(telemetriaDataTextBox.Text);
                if(deg > MAX_SERVO_ANGLE || deg < 0 )
                {
                    updateRadarStatusLabel("servo degree range error");
                    return;
                }
                telemetriaAngleLabel.Text = "Angle: " + telemetriaDataTextBox.Text;
                serialPort.sendMessage(CustomSerialPort.TYPE.TELMETERIA, telemetriaDataTextBox.Text);
                telemetriaDataTextBox.Text = "";
                updateRadarStatusLabel("");
                scanButton.Visible = false;
                stopTelemetriaButton.Visible = true;
            }
            catch (Exception)
            {
                if(serialPort.IsOpen)
                    updateRadarStatusLabel("servo degree accepts only integers in range [0,180]");
                else
                    updateRadarStatusLabel("configure serial port");
            }
        }

        /*
         * Scan Radar Button Click Listener
         */ 
        private void scanButton_Click(object sendMessage, EventArgs e)
        {
            if(!serialPort.IsOpen)
            {
                updateRadarStatusLabel("configure serial port");
                return;
            }
            if (scanMode) // Stop scanning
            {
                scanMode = false;
                if (!sendMessage.Equals("false"))
                {
                    try
                    {
                        serialPort.sendMessage(CustomSerialPort.TYPE.STOP_RADAR, "");
                        Thread.Sleep(100);
                        serialPort.clearMyBuffer();
                        setConnectingLabel(CustomSerialPort.STATUS.OK);
                    }
                    catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }
                }
                EraseLines();
                scanButton.Text = "Start Scan";
                radarPictureBox.Visible = false;
                radarTextPanel.Visible = false;
                deg150Label.Visible = false;
                deg120Label.Visible = false;
                deg90Label.Visible = false;
                deg60Label.Visible = false;
                deg30Label.Visible = false;
                // Telemetria On
                telemetriaButton.Visible = true;
                telemetriaDataTextBox.Visible = true;
                telemetriaLabel.Visible = true;
                telemetriaPanel.Visible = true;
                telemetriaEnterAngleLabel.Visible = true;
                radarPanel.BackColor = Color.Transparent;
            }
            else // start scanning
            {
                scanMode = true;
                if (!sendMessage.Equals("false"))
                {
                    try
                    {
                        serialPort.sendMessage(CustomSerialPort.TYPE.SCAN, "");

                    }
                    catch (Exception) { setConnectingLabel(CustomSerialPort.STATUS.PORT_ERROR); }
                }
                scanButton.Text = "Stop Scan";
                radarPictureBox.Visible = true;
                radarTextPanel.Visible = true;
                deg150Label.Visible = true;
                deg120Label.Visible = true;
                deg90Label.Visible = true;
                deg60Label.Visible = true;
                deg30Label.Visible = true;
                // Telemetria Off
                telemetriaButton.Visible = false;
                telemetriaDataTextBox.Visible = false;
                telemetriaLabel.Visible = false;
                telemetriaPanel.Visible = false;
                telemetriaEnterAngleLabel.Visible = false;
                stopTelemetriaButton.Visible = false;
                radarPanel.BackColor = Color.Black;
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
            initializeFilesListView();

        }

        /*
         * Save Script Button Click
         */ 
        private void saveScriptButton_Click(object sender, EventArgs e)
        {
            if (scriptNameTextBox.Text.Equals("") || scriptDataRichTextBox.Text.Equals(""))
                return;
            string path = Path.Combine(dataFilesPath, scriptNameTextBox.Text + ".txt");
            string text = scriptDataRichTextBox.Text;
            text = text.Replace("\n","\r\n");
            File.WriteAllText(path, text);
            filesListView.Clear();
            initializeFilesListView();
            scriptNameTextBox.Text = "";
            scriptDataRichTextBox.Text = "";
        }


        /*
         * Stop Telemetria Button
         */ 
        private void stopTelemetriaButton_Click(object sender, EventArgs e)
        {
            serialPort.sendMessage(CustomSerialPort.TYPE.STOP_RADAR,"");
            stopTelemetriaButton.Visible = false;
            scanButton.Visible = true;
            serialPort.clearMyBuffer();
            setConnectingLabel(CustomSerialPort.STATUS.OK);
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
                        updateRadarStatusLabel("");
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
            {
                updateFileTransferStatusLabel("select a file");
                return;
            }

            if (!serialPort.IsOpen)
            {
                updateFileTransferStatusLabel("configure serial port");
                return;
            }


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
