using System;
using System.Windows.Forms;
using EsploraPulse.View;

namespace EsploraPulse
{
    /// <summary>
    /// This class is responsible for executing the application.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/28/2015</version>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EsploraPulseForm());
        }
    }
}
