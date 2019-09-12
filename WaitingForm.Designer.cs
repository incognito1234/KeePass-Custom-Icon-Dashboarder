namespace CustomIconDashboarderPlugin
{
    partial class WaitingForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.lbl_waitingMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_waitingMessage
            // 
            this.lbl_waitingMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_waitingMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.900001F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_waitingMessage.Location = new System.Drawing.Point(0, 0);
            this.lbl_waitingMessage.Name = "lbl_waitingMessage";
            this.lbl_waitingMessage.Size = new System.Drawing.Size(794, 300);
            this.lbl_waitingMessage.TabIndex = 0;
            this.lbl_waitingMessage.Text = "Text";
            this.lbl_waitingMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WaitingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 300);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_waitingMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WaitingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Please wait...";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_waitingMessage;
    }
}