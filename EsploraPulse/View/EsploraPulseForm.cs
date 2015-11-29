using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
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
        private const string PortName = "COM4";
        private const int BaudRate = 115200;

        private readonly EsploraPulseController controller;

        private readonly PulseReading reading;
        private readonly PulseSensorData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsploraPulseForm"/> class.
        /// </summary>
        public EsploraPulseForm()
        {
            this.InitializeComponent();

            this.reading = new PulseReading();
            this.data = new PulseSensorData();

            this.controller = new EsploraPulseController(ref this.reading, ref this.data);

            this.EsploraSerial.PortName = PortName;
            this.EsploraSerial.BaudRate = BaudRate;
            this.EsploraSerial.DtrEnable = true;

        }

        private void onDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int serialData;
                int.TryParse(this.EsploraSerial.ReadLine(), out serialData);

                this.data.Signal = serialData;
                this.controller.CalculatePulse();

                this.Invoke((MethodInvoker) delegate
                {
                    this.PulseLabel.Text = this.reading.BPM.ToString();
                });
            }
            catch (Exception exception)
            {
                throw exception;
            }
            
        }

        private void EsploraPulseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.EsploraSerial.IsOpen)
            {
                this.EsploraSerial.Close();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.EsploraSerial.Open();

                this.startButton.Enabled = false;
                this.stopButton.Enabled = true;
                this.emailButton.Enabled = false;
            }
            catch (IOException ioException)
            {
                ExceptionHandler.DisplayErrorMessage("Serial Port Error", ioException.Message);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                ExceptionHandler.DisplayErrorMessage("Serial Port Busy", unauthorizedAccessException.Message);
            } 

            this.EsploraSerial.DataReceived += this.onDataReceived;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Enabled = false;
            this.startButton.Enabled = true;
            this.emailButton.Enabled = true;

            this.EsploraSerial.DataReceived -= this.onDataReceived;
            this.EsploraSerial.Close();
        }

        private void emailButton_Click(object sender, EventArgs e)
        {
            var emailForm = new EmailForm(this.reading.BPM);
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
