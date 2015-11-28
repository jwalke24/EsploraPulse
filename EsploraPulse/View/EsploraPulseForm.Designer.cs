namespace EsploraPulse.View
{
    partial class EsploraPulseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PulseLabel = new System.Windows.Forms.Label();
            this.EsploraSerial = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // PulseLabel
            // 
            this.PulseLabel.AutoSize = true;
            this.PulseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PulseLabel.Location = new System.Drawing.Point(54, 116);
            this.PulseLabel.Name = "PulseLabel";
            this.PulseLabel.Size = new System.Drawing.Size(168, 20);
            this.PulseLabel.TabIndex = 0;
            this.PulseLabel.Text = "<Pulse Displays Here>";
            // 
            // EsploraPulseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.PulseLabel);
            this.Name = "EsploraPulseForm";
            this.Text = "Esplora Pulse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EsploraPulseForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PulseLabel;
        private System.IO.Ports.SerialPort EsploraSerial;
    }
}

