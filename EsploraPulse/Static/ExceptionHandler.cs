using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsploraPulse.Static
{
    /// <summary>
    /// This class is responsible for presenting exceptions to the user
    /// in a "friendly" manner.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    static class ExceptionHandler
    {
        /// <summary>
        /// Displays the error message in a MessageBox.
        /// </summary>
        /// <param name="e">The exception.</param>
        public static void DisplayErrorMessage(Exception e)
        {
            MessageBox.Show(e.ToString(), e.Message);
        }
    }
}
