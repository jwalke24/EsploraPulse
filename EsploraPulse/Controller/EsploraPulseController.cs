using System;
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
        private readonly PulseCalculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsploraPulseController" /> class.
        /// </summary>
        public EsploraPulseController(ref PulseReading reading, ref PulseSensorData data)
        {
            this.calculator = new PulseCalculator(ref reading, ref data);
        }

        /// <summary>
        /// Calculates the pulse.
        /// </summary>
        public void CalculatePulse()
        {
            this.calculator.CalculatePulse();
        }
    }
}
