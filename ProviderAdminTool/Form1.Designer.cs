namespace ProviderAdminTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.accountsDB = new System.Windows.Forms.DataGridView();
            this.AssignedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhatsAppId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.LoadAccountsBTN = new System.Windows.Forms.Button();
            this.CreateAccountBTN = new System.Windows.Forms.Button();
            this.DeleteAccountBTN = new System.Windows.Forms.Button();
            this.UpdateAccountBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.accountsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // accountsDB
            // 
            this.accountsDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.accountsDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssignedTo,
            this.PhoneNumber,
            this.WhatsAppId,
            this.RoleID});
            this.accountsDB.Location = new System.Drawing.Point(12, 70);
            this.accountsDB.Name = "accountsDB";
            this.accountsDB.RowTemplate.Height = 25;
            this.accountsDB.Size = new System.Drawing.Size(843, 368);
            this.accountsDB.TabIndex = 0;
            this.accountsDB.EnabledChanged += new System.EventHandler(this.AccountsDB_EnableChanged);
            // 
            // AssignedTo
            // 
            this.AssignedTo.HeaderText = "Assigned To:";
            this.AssignedTo.Name = "AssignedTo";
            this.AssignedTo.Width = 300;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.HeaderText = "SMS Phone Number:";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.Width = 200;
            // 
            // WhatsAppId
            // 
            this.WhatsAppId.HeaderText = "WhatsApp Phone Number ID:";
            this.WhatsAppId.Name = "WhatsAppId";
            this.WhatsAppId.Width = 200;
            // 
            // RoleID
            // 
            this.RoleID.HeaderText = "Role ID:";
            this.RoleID.Name = "RoleID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email To Lookup:";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(132, 12);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(723, 23);
            this.emailTB.TabIndex = 6;
            // 
            // LoadAccountsBTN
            // 
            this.LoadAccountsBTN.Location = new System.Drawing.Point(132, 41);
            this.LoadAccountsBTN.Name = "LoadAccountsBTN";
            this.LoadAccountsBTN.Size = new System.Drawing.Size(126, 23);
            this.LoadAccountsBTN.TabIndex = 7;
            this.LoadAccountsBTN.Text = "Load Account(s)";
            this.LoadAccountsBTN.UseVisualStyleBackColor = true;
            this.LoadAccountsBTN.Click += new System.EventHandler(this.LoadAccounts_Click);
            // 
            // CreateAccountBTN
            // 
            this.CreateAccountBTN.Location = new System.Drawing.Point(264, 41);
            this.CreateAccountBTN.Name = "CreateAccountBTN";
            this.CreateAccountBTN.Size = new System.Drawing.Size(140, 23);
            this.CreateAccountBTN.TabIndex = 8;
            this.CreateAccountBTN.Text = "Create a New Account";
            this.CreateAccountBTN.UseVisualStyleBackColor = true;
            this.CreateAccountBTN.Click += new System.EventHandler(this.Button2_Click);
            // 
            // DeleteAccountBTN
            // 
            this.DeleteAccountBTN.Location = new System.Drawing.Point(572, 41);
            this.DeleteAccountBTN.Name = "DeleteAccountBTN";
            this.DeleteAccountBTN.Size = new System.Drawing.Size(145, 23);
            this.DeleteAccountBTN.TabIndex = 9;
            this.DeleteAccountBTN.Text = "Delete Selected Account";
            this.DeleteAccountBTN.UseVisualStyleBackColor = true;
            this.DeleteAccountBTN.Click += new System.EventHandler(this.DeleteAccountBTN_Click);
            // 
            // UpdateAccountBTN
            // 
            this.UpdateAccountBTN.Location = new System.Drawing.Point(410, 41);
            this.UpdateAccountBTN.Name = "UpdateAccountBTN";
            this.UpdateAccountBTN.Size = new System.Drawing.Size(156, 23);
            this.UpdateAccountBTN.TabIndex = 10;
            this.UpdateAccountBTN.Text = "Update Selected Account";
            this.UpdateAccountBTN.UseVisualStyleBackColor = true;
            this.UpdateAccountBTN.Click += new System.EventHandler(this.UpdateAccountBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 450);
            this.Controls.Add(this.UpdateAccountBTN);
            this.Controls.Add(this.DeleteAccountBTN);
            this.Controls.Add(this.CreateAccountBTN);
            this.Controls.Add(this.LoadAccountsBTN);
            this.Controls.Add(this.emailTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountsDB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Provider Admin Tool";
            this.Load += new System.EventHandler(this.Formload);
            ((System.ComponentModel.ISupportInitialize)(this.accountsDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView accountsDB;
        private Label label3;
        private TextBox emailTB;
        private Button LoadAccountsBTN;
        private Button CreateAccountBTN;
        private Button DeleteAccountBTN;
        private Button UpdateAccountBTN;
        private DataGridViewTextBoxColumn AssignedTo;
        private DataGridViewTextBoxColumn PhoneNumber;
        private DataGridViewTextBoxColumn WhatsAppId;
        private DataGridViewTextBoxColumn RoleID;
    }
}