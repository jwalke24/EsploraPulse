using System;

namespace EsploraPulse.Model
{
    /// <summary>
    /// This class represents a reading of heart rate information, including
    /// beats per minute, the interbeat interval, whether a pulse exists, and
    /// whether a heart beat is presently detected.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    class PulseReading
    {
        private int bpm;
        private int ibi;

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseReading" /> class.
        /// </summary>
        public PulseReading()
        {
            this.BPM = 0;
            this.IBI = 600;
            this.HeartBeatExists = false;
            this.HeartBeatDetected = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a heart beat is present.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this a heart beat is present; otherwise, <c>false</c>.
        /// </value>
        public bool HeartBeatExists { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a heart beat has been detected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if heart beat is detected; otherwise, <c>false</c>.
        /// </value>
        public bool HeartBeatDetected { get; set; }

        /// <summary>
        /// Gets the beats per minute of the reading. This value represents the number of times a heart beats
        /// in a span of one minute. This number should be lower when a person is resting and higher when
        /// a person is exercising and/or emotional.
        /// </summary>
        /// <value>
        /// The beats per minute.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@bpm must be greater than or equal to zero</exception>
        public int BPM
        {
            get { return this.bpm; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"bpm must be greater than or equal to zero");
                }
                this.bpm = value;
            }
        }

        /// <summary>
        /// Gets the interbeat interval of the reading. This value represents the time, in milliseconds, between
        /// individual heart beats. This value varies naturally.
        /// </summary>
        /// <value>
        /// The interbeat interval.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">@ibi must be greater than or equal to zero</exception>
        public int IBI
        {
            get { return this.ibi; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"ibi must be greater than or equal to zero");
                }
                this.ibi = value;
            }
        }
    }
}
