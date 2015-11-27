using EsploraPulse.Model;

namespace EsploraPulse.Controller
{
    /// <summary>
    /// This class is responsible for creating/managing PulseReading objects and
    /// the SerialReader object.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    class EsploraPulseController
    {
        private SerialReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsploraPulseController"/> class.
        /// </summary>
        public EsploraPulseController()
        {
            this.reader = new SerialReader();
        }
    }
}
