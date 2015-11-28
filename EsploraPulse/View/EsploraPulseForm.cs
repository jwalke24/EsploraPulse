using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using EsploraPulse.Controller;
using EsploraPulse.Model;

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

        private EsploraPulseController controller;

        private PulseReading reading;
        private PulseSensorData data;

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
            this.data.Signal = int.Parse(this.EsploraSerial.ReadLine());
            this.controller.CalculatePulse();

            this.Invoke((MethodInvoker) delegate
            {
                this.PulseLabel.Text = this.reading.BPM.ToString();
            });
        }

        private void EsploraPulseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.EsploraSerial.IsOpen)
            {
                this.EsploraSerial.Close();
            }
        }

        private void howToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Place your finger within the cuff without pressing on the sensor too hard. Wait for the BPM reading to stabilize around a value. That value is your heart rate.", @"How To...",
                MessageBoxButtons.OK);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;

            this.EsploraSerial.Open();
            this.EsploraSerial.DataReceived += this.onDataReceived;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Enabled = false;
            this.startButton.Enabled = true;

            this.EsploraSerial.DataReceived -= this.onDataReceived;

        }
    }
}
