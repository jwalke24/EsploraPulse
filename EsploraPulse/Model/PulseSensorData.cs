using System;

namespace EsploraPulse.Model
{
    /// <summary>
    /// This class contains data needed by the Pulse Sensor calculations.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    class PulseSensorData
    {
        private const int RateCapacity = 10;

        private int threshold;
        private int signal;
        private readonly int[] rate;
        private int peak;
        private int trough;
        private int amplitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseSensorData"/> class.
        /// </summary>
        public PulseSensorData()
        {
            this.rate = new int[RateCapacity];
            this.SampleCounter = 0;
            this.LastBeatTime = 0;
            this.Peak = 512;
            this.Trough = 512;
            this.Threshold = 525;
            this.Amplitude = 100;
            this.FirstBeat = true;
            this.SecondBeat = false;
        }

        /// <summary>
        /// Sets the rate at the index to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// @rate to set must be greater than or equal to zero
        /// or
        /// @set rate index out of bounds
        /// </exception>
        public void SetRateByIndex(int value, int index)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), @"rate to set must be greater than or equal to zero");
            }

            if (index < 0 || index >= RateCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(index), @"set rate index out of bounds");
            }
            this.rate[index] = value;

        }

        /// <summary>
        /// Gets the rate at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The rate at the index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">@get rate index out of bounds</exception>
        public int GetRateByIndex(int index)
        {
            if (index < 0 || index >= RateCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(index), @"get rate index out of bounds");
            }

            return this.rate[index];
        }

        /// <summary>
        /// Gets or sets the threshold.
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@threshold must be greater than or equal to zero</exception>
        public int Threshold
        {
            get { return this.threshold; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"threshold must be greater than or equal to zero");
                }
                this.threshold = value;
            }
        }

        /// <summary>
        /// Gets or sets the signal from the Pulse Sensor.
        /// </summary>
        /// <value>
        /// The signal from the pulse sensor.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@signal must be greater than or equal to zero</exception>
        public int Signal
        {
            get { return this.signal; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"signal must be greater than or equal to zero");
                }
                this.signal = value;
            }
        }

        /// <summary>
        /// Gets or sets value, in milliseconds, representing pulse timing.
        /// </summary>
        /// <value>
        /// The sample counter.
        /// </value>
        public ulong SampleCounter { get; set; }

        /// <summary>
        /// Gets or sets the time of the last heart beat.
        /// </summary>
        /// <value>
        /// The last beat time.
        /// </value>
        public ulong LastBeatTime { get; set; }

        /// <summary>
        /// Gets or sets the peak.
        /// </summary>
        /// <value>
        /// The peak.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@peak must be greater than or equal to zero</exception>
        public int Peak
        {
            get { return this.peak; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"peak must be greater than or equal to zero");
                }
                this.peak = value;
            }
        }

        /// <summary>
        /// Gets or sets the trough.
        /// </summary>
        /// <value>
        /// The trough.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@trough must be greater than or equal to zero</exception>
        public int Trough
        {
            get { return this.trough; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"trough must be greater than or equal to zero");
                }
                this.trough = value;
            }
        }

        /// <summary>
        /// Gets or sets the amplitude.
        /// </summary>
        /// <value>
        /// The amplitude.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@amplitude must be greater than or equal to zero</exception>
        public int Amplitude
        {
            get { return this.amplitude; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"amplitude must be greater than or equal to zero");
                }
                this.amplitude = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [first beat].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [first beat]; otherwise, <c>false</c>.
        /// </value>
        public bool FirstBeat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [second beat].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [second beat]; otherwise, <c>false</c>.
        /// </value>
        public bool SecondBeat { get; set; }
    }
}
