using System;

namespace EsploraPulse.Model
{
    /// <summary>
    /// This class is responsible for calculating the pulse.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    class PulseCalculator
    {
        private readonly PulseData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseCalculator"/> class.
        /// </summary>
        /// <param name="data">The data object.</param>
        /// <exception cref="ArgumentNullException">
        /// @the pulse sensor data object cannot be null
        /// </exception>
        public PulseCalculator(ref PulseData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), @"the pulse sensor data object cannot be null");
            }
            
            this.data = data;
        }

        /// <summary>
        /// Calculates the pulse.
        /// </summary>
        public void CalculatePulse()
        {
            this.data.SampleCounter += 50;
            var n = (int)(this.data.SampleCounter - this.data.LastBeatTime);

            this.calculateTrough(n);
            this.calculatePeak();
            
            if (n > 250)
            {
                this.handlePulse(n);   
            }

            if (this.data.Signal < this.data.Threshold && this.data.HeartBeatExists)
            {
                this.data.HeartBeatExists = false;
                this.data.Amplitude = this.data.Peak - this.data.Trough;
                this.data.Threshold = this.data.Amplitude/2 + this.data.Trough;
                this.data.Peak = this.data.Threshold;
                this.data.Trough = this.data.Threshold;
            }

            if (n > 2500)
            {
                this.data.Threshold = 512;
                this.data.Peak = this.data.Threshold;
                this.data.Trough = this.data.Threshold;
                this.data.LastBeatTime = this.data.SampleCounter;
                this.data.FirstBeat = true;
                this.data.SecondBeat = false;
            }
        }

        private void handlePulse(int n)
        {
            if ((this.data.Signal > this.data.Threshold) && (this.data.HeartBeatExists == false) &&
                    (n > this.data.IBI / 5 * 3))
            {
                this.data.HeartBeatExists = true;
                this.data.IBI = (int)(this.data.SampleCounter - this.data.LastBeatTime);
                this.data.LastBeatTime = this.data.SampleCounter;

                if (this.data.SecondBeat)
                {
                    this.data.SecondBeat = false;
                    for (var i = 0; i <= 9; i++)
                    {
                        this.data.SetRateByIndex(this.data.IBI, i);
                    }
                }

                if (this.data.FirstBeat)
                {
                    this.data.FirstBeat = false;
                    this.data.SecondBeat = true;
                    return;
                }

                uint runningTotal = 0;

                for (var i = 0; i <= 8; i++)
                {
                    this.data.SetRateByIndex(this.data.GetRateByIndex(i + 1), i);
                    runningTotal += (uint)this.data.GetRateByIndex(i);
                }

                this.data.SetRateByIndex(this.data.IBI, 9);
                runningTotal += (uint)this.data.GetRateByIndex(9);
                runningTotal /= 10;
                this.data.BPM = 60000 / (int)runningTotal;
                this.data.HeartBeatDetected = true;
            }
        }

        private void calculatePeak()
        {
            if (this.data.Signal > this.data.Threshold && this.data.Signal > this.data.Peak)
            {
                this.data.Peak = this.data.Signal;
            }
        }

        private void calculateTrough(int n)
        {
            if (this.data.Signal >= this.data.Threshold || n <= this.data.IBI/5*3)
            {
                return;
            }

            if (this.data.Signal < this.data.Trough)
            {
                this.data.Trough = this.data.Signal;
            }
        }
    }
}
