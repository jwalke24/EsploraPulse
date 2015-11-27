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

        private SerialPort port;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialReader"/> class.
        /// </summary>
        public SerialReader()
        {
            this.port = new SerialPort(PortName, BaudRate);
        }
    }
}
