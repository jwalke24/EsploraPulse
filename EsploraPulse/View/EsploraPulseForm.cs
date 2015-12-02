using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EsploraPulse.Controller;
using EsploraPulse.Model;
using EsploraPulse.Static;

namespace EsploraPulse.View
{
    /// <summary>
    /// This class represents the code-behind for the EsploraPulse form,
    /// which is responsible for displaying PulseReadings.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    public partial class EsploraPulseForm : Form
    {
        // The Serial Port name to read from. Ensure that this name matches the PortName from the Arduino code.
        private const string PortName = "COM4";

        // The rate at which to read data from the Serial Port. Ensure that this value matches the BaudRate from the Arduino code.
        private const int BaudRate = 9600;

        // The maximum number of data points to display on the chart at any one time.
        private const int AxisXMax = 100;

        private readonly EsploraPulseController controller;
        
        private readonly PulseData data;

        // The data for the chart.
        private Series bpmSeries;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsploraPulseForm"/> class.
        /// </summary>
        public EsploraPulseForm()
        {
            this.InitializeComponent();
            
            this.data = new PulseData();
            this.controller = new EsploraPulseController(ref this.data);

            // Set up the Serial Port.
            this.EsploraSerial.PortName = PortName;
            this.EsploraSerial.BaudRate = BaudRate;
            this.EsploraSerial.DtrEnable = true;
        }

        private void EsploraPulseForm_Load(object sender, EventArgs e)
        {
            // Set up the Chart.
            this.intializePulseChart();
        }

        private void intializePulseChart()
        {
            this.pulseChart.Series.Clear();

            this.bpmSeries = new Series("BPM");
            this.pulseChart.Series.Add(this.bpmSeries);
            this.bpmSeries.XValueType = ChartValueType.Int32;
            this.bpmSeries.YValueType = ChartValueType.Int32;
            this.bpmSeries.ChartType = SeriesChartType.Spline;
            this.pulseChart.ChartAreas[this.bpmSeries.ChartArea].AxisX.Title = "Time (milliseconds)";
            this.pulseChart.ChartAreas[this.bpmSeries.ChartArea].AxisY.Title = "Sensor Reading";
            this.bpmSeries.Color = Color.Red;
        }

        /// <summary>
        /// This code executes when the SerialPort receives data (sensor reading).
        /// The BPM is calculated, displayed on the screen, and a new data point is added to the chart.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SerialDataReceivedEventArgs"/> instance containing the event data.</param>
        private void onDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int serialData;
                int.TryParse(this.EsploraSerial.ReadLine(), out serialData);

                this.data.Signal = serialData;
                this.controller.CalculatePulse();

                BeginInvoke((MethodInvoker) delegate
                {
                    this.PulseLabel.Text = this.data.BPM.ToString();

                    if (this.bpmSeries.Points.Count > AxisXMax)
                    {
                        this.bpmSeries.Points.RemoveAt(0);
                    }

                    this.bpmSeries.Points.AddXY(this.data.SampleCounter, this.data.Signal);
                    this.pulseChart.ResetAutoValues();
                    
                });
            }
            catch (Exception exception)
            {
                ErrorHandler.DisplayErrorMessage("Error", exception.Message);
            }
        }

        private void EsploraPulseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // When the "X" button is clicked, close the Serial Port.
            this.EsploraSerial.Close();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Opens the Serial Port if it is not open already. Either way, the onDataReceived event is attached to the Serial Port.
            if (this.EsploraSerial.IsOpen == false)
            {
                try
                {
                    this.EsploraSerial.Open();
                    this.EsploraSerial.DataReceived += this.onDataReceived;
                }
                catch (IOException)
                {
                    ErrorHandler.DisplayErrorMessage("Serial Port Error",
                        "Please plug the Arduino Esplora into the " + this.EsploraSerial.PortName + " serial port.");
                    return;
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    ErrorHandler.DisplayErrorMessage("Serial Port Busy", unauthorizedAccessException.Message);
                    return;
                }
            }
            else
            {
                this.EsploraSerial.DataReceived += this.onDataReceived;
            }

            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;
            this.emailButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Enabled = false;
            this.startButton.Enabled = true;
            this.emailButton.Enabled = true;

            // Detach the onDataReceivedEvent from the Serial Port to pause the readings.
            this.EsploraSerial.DataReceived -= this.onDataReceived;
        }

        private void emailButton_Click(object sender, EventArgs e)
        {
            // Create an email form and display it.
            var emailForm = new EmailForm(this.data.BPM);
            var result = emailForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                MessageBox.Show(@"Message Sent!", @"Success");
            }
        }

        private void readBPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Place your finger within the cuff without pressing on the sensor too hard. " +
                    @"Wait for the BPM reading to stabilize around a value. That value is your heart rate.", 
                @"How To: Read BPM", MessageBoxButtons.OK);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"First press the Start button to begin reading your heart rate. Once you have received your BPM value, click Stop. " +
                    @"At this point, you will be able to click Send Email and email your BPM to anyone using Gmail as a SMTP host.",
                @"How To: Send Email", MessageBoxButtons.OK);
        }
    }
}
