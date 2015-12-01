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
        public EsploraPulseController(ref PulseData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), @"The data object cannot be null.");
            }
            this.calculator = new PulseCalculator(ref data);
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
