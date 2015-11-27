using System.Windows.Forms;
using EsploraPulse.Controller;

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
        private EsploraPulseController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsploraPulseForm"/> class.
        /// </summary>
        public EsploraPulseForm()
        {
            this.InitializeComponent();

            this.controller = new EsploraPulseController();
        }
    }
}
