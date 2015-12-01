using System.Windows.Forms;

namespace EsploraPulse.Static
{
    /// <summary>
    /// This class is responsible for presenting exceptions to the user
    /// in a "friendly" manner.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    static class ErrorHandler
    {
        /// <summary>
        /// Displays the error message in a MessageBox.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public static void DisplayErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
