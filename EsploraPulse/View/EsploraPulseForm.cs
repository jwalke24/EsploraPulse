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
            this.EsploraSerial.Open();
            this.EsploraSerial.DataReceived += this.onDataReceived;

        }

        private void onDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.data.Signal = int.Parse(this.EsploraSerial.ReadLine());
            this.controller.CalculatePulse();
            Console.WriteLine(this.reading.BPM);
        }

        private void EsploraPulseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.EsploraSerial.Close();
        }
    }
}
