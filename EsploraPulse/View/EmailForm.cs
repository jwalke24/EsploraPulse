using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using EsploraPulse.Static;

namespace EsploraPulse.View
{
    /// <summary>
    /// This class represents a form used for sending an email.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/28/2015</version>
    public partial class EmailForm : Form
    {
        private readonly int bpm;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailForm"/> class.
        /// </summary>
        /// <param name="bpm">The BPM or heart rate to email.</param>
        /// <exception cref="ArgumentOutOfRangeException">@bpm must be greater than or equal to zero</exception>
        public EmailForm(int bpm)
        {
            if (bpm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bpm), @"bpm must be greater than or equal to zero");
            }

            InitializeComponent();

            this.bpm = bpm;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.sendBPMEmail();
            }
            catch (ArgumentException)
            {
                ExceptionHandler.DisplayErrorMessage("Invalid Input", "Please enter information into every text box.");
            }
            catch (FormatException)
            {
                ExceptionHandler.DisplayErrorMessage("Invalid Input",
                    "Please ensure that your email addresses meet the formatting requirements of email addresses (i.e. address@host.domain).");
            }
            catch (SmtpException)
            {
                ExceptionHandler.DisplayErrorMessage("Authentication Error",
                    "There was a problem authenticating your email account. Please ensure your password is correct. " +
                    "If the password is correct, Gmail may be blocking this app from accessing your email. Please check your email for further instructions.");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sendBPMEmail()
        {
            var dateAndTime = DateTime.Now;

            var fromAddress = new MailAddress(this.fromTextBox.Text);
            var toAddress = new MailAddress(this.toTextBox.Text);
            var fromPassword = this.passwordTextBox.Text;
            var subject = dateAndTime.ToString("yyyy MMMM dd") + ": Heart Rate";
            var body = "My heart rate was " + this.bpm + " on " + dateAndTime.ToString("yyyy MMMM dd") + " at " + dateAndTime.ToString("hh:mm tt");

            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                client.Send(message);
            }

            client.Dispose();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
