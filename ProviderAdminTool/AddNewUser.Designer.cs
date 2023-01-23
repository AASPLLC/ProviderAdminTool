namespace ProviderAdminTool
{
    partial class AddNewUser
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
            this.whatsappIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.smsNumberTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // whatsappIDTB
            // 
            this.whatsappIDTB.Location = new System.Drawing.Point(181, 70);
            this.whatsappIDTB.Name = "whatsappIDTB";
            this.whatsappIDTB.Size = new System.Drawing.Size(607, 23);
            this.whatsappIDTB.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "WhatsApp Phone Number ID:";
            // 
            // smsNumberTB
            // 
            this.smsNumberTB.Location = new System.Drawing.Point(181, 41);
            this.smsNumberTB.Name = "smsNumberTB";
            this.smsNumberTB.Size = new System.Drawing.Size(607, 23);
            this.smsNumberTB.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "SMS Phone Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Email:";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(181, 12);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(607, 23);
            this.emailTB.TabIndex = 7;
            this.emailTB.Text = "asdf@asdf.com";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Create Account";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // AddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 133);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.whatsappIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.smsNumberTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.emailTB);
            this.Name = "AddNewUser";
            this.Text = "AddNewUser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox whatsappIDTB;
        private Label label3;
        private TextBox smsNumberTB;
        private Label label2;
        private Label label1;
        private TextBox emailTB;
        private Button button1;
    }
}