namespace UPDChat
{
    partial class ServerLogForm
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
            this.ServerLogTxt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ServerLogTxt
            // 
            this.ServerLogTxt.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.ServerLogTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerLogTxt.Location = new System.Drawing.Point(0, 0);
            this.ServerLogTxt.Name = "ServerLogTxt";
            this.ServerLogTxt.ReadOnly = true;
            this.ServerLogTxt.Size = new System.Drawing.Size(749, 261);
            this.ServerLogTxt.TabIndex = 0;
            this.ServerLogTxt.Text = "";
            // 
            // ServerLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 261);
            this.Controls.Add(this.ServerLogTxt);
            this.Name = "ServerLogForm";
            this.Text = "ServerLogForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerLogForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox ServerLogTxt;

    }
}