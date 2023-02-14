namespace ProviderAdminTool
{
    partial class SelectTool
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
            this.AccountsBTN = new System.Windows.Forms.Button();
            this.ProfilesBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AccountsBTN
            // 
            this.AccountsBTN.Location = new System.Drawing.Point(12, 12);
            this.AccountsBTN.Name = "AccountsBTN";
            this.AccountsBTN.Size = new System.Drawing.Size(84, 43);
            this.AccountsBTN.TabIndex = 0;
            this.AccountsBTN.Text = "Accounts\r\n(Employees)";
            this.AccountsBTN.UseVisualStyleBackColor = true;
            this.AccountsBTN.Click += new System.EventHandler(this.AccountsBTN_Click);
            // 
            // ProfilesBTN
            // 
            this.ProfilesBTN.Location = new System.Drawing.Point(102, 12);
            this.ProfilesBTN.Name = "ProfilesBTN";
            this.ProfilesBTN.Size = new System.Drawing.Size(84, 43);
            this.ProfilesBTN.TabIndex = 1;
            this.ProfilesBTN.Text = "Profiles\r\n(End Users)";
            this.ProfilesBTN.UseVisualStyleBackColor = true;
            this.ProfilesBTN.Click += new System.EventHandler(this.ProfilesBTN_Click);
            // 
            // SelectTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 67);
            this.Controls.Add(this.ProfilesBTN);
            this.Controls.Add(this.AccountsBTN);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTool";
            this.Text = "Provider Admin Tool";
            this.ResumeLayout(false);

        }

        #endregion

        private Button AccountsBTN;
        private Button ProfilesBTN;
    }
}