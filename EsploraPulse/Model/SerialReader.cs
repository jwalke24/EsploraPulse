using System;
using System.IO.Ports;

namespace EsploraPulse.Model
{
    /// <summary>
    /// This class is responsible for reading data from
    /// the Arduino Esplora via a SerialPort object.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    class SerialReader
    {
        private const string PortName = "COM4";
        private const int BaudRate = 115200;

        private PulseReading reading;
        private PulseSensorData data;

        private SerialPort port;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialReader" /> class.
        /// </summary>
        /// <param name="reading">The pulse reading object.</param>
        /// <param name="data">The sensor data object.</param>
        public SerialReader(ref PulseReading reading, ref PulseSensorData data)
        {
            this.reading = reading;
            this.data = data;
            this.port = new SerialPort(PortName, BaudRate) {DtrEnable = true};

            this.port.Open();

            this.port.DataReceived += this.onDataReceived;
        }

        private void onDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.data.Signal = int.Parse(this.port.ReadLine());
        }
    }
}
