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
            this.label2 = new System.Windows.Forms.Label();
            this.vaultTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.emailTB = new System.Windows.Forms.TextBox();
            this.loadAccountBTN = new System.Windows.Forms.Button();
            this.createAccountBTN = new System.Windows.Forms.Button();
            this.deleteAccountBTN = new System.Windows.Forms.Button();
            this.updateAccountBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.accountsDB)).BeginInit();
            this.SuspendLayout();
            // 
            // accountsDB
            // 
            this.accountsDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accountsDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AssignedTo,
            this.PhoneNumber,
            this.WhatsAppId});
            this.accountsDB.Location = new System.Drawing.Point(12, 93);
            this.accountsDB.Name = "accountsDB";
            this.accountsDB.RowTemplate.Height = 25;
            this.accountsDB.Size = new System.Drawing.Size(743, 345);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vault Name:";
            // 
            // vaultTB
            // 
            this.vaultTB.Location = new System.Drawing.Point(132, 6);
            this.vaultTB.Name = "vaultTB";
            this.vaultTB.Size = new System.Drawing.Size(623, 23);
            this.vaultTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email To Lookup:";
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(132, 35);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(623, 23);
            this.emailTB.TabIndex = 6;
            // 
            // loadAccountBTN
            // 
            this.loadAccountBTN.Location = new System.Drawing.Point(109, 64);
            this.loadAccountBTN.Name = "loadAccountBTN";
            this.loadAccountBTN.Size = new System.Drawing.Size(126, 23);
            this.loadAccountBTN.TabIndex = 7;
            this.loadAccountBTN.Text = "Load Account(s)";
            this.loadAccountBTN.UseVisualStyleBackColor = true;
            this.loadAccountBTN.Click += new System.EventHandler(this.Button1_Click);
            // 
            // createAccountBTN
            // 
            this.createAccountBTN.Location = new System.Drawing.Point(241, 64);
            this.createAccountBTN.Name = "createAccountBTN";
            this.createAccountBTN.Size = new System.Drawing.Size(140, 23);
            this.createAccountBTN.TabIndex = 8;
            this.createAccountBTN.Text = "Create a New Account";
            this.createAccountBTN.UseVisualStyleBackColor = true;
            this.createAccountBTN.Click += new System.EventHandler(this.Button2_Click);
            // 
            // deleteAccountBTN
            // 
            this.deleteAccountBTN.Location = new System.Drawing.Point(549, 64);
            this.deleteAccountBTN.Name = "deleteAccountBTN";
            this.deleteAccountBTN.Size = new System.Drawing.Size(145, 23);
            this.deleteAccountBTN.TabIndex = 9;
            this.deleteAccountBTN.Text = "Delete Selected Account";
            this.deleteAccountBTN.UseVisualStyleBackColor = true;
            this.deleteAccountBTN.Click += new System.EventHandler(this.DeleteAccountBTN_Click);
            // 
            // updateAccountBTN
            // 
            this.updateAccountBTN.Location = new System.Drawing.Point(387, 64);
            this.updateAccountBTN.Name = "updateAccountBTN";
            this.updateAccountBTN.Size = new System.Drawing.Size(156, 23);
            this.updateAccountBTN.TabIndex = 10;
            this.updateAccountBTN.Text = "Update Selected Account";
            this.updateAccountBTN.UseVisualStyleBackColor = true;
            this.updateAccountBTN.Click += new System.EventHandler(this.UpdateAccountBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 450);
            this.Controls.Add(this.updateAccountBTN);
            this.Controls.Add(this.deleteAccountBTN);
            this.Controls.Add(this.createAccountBTN);
            this.Controls.Add(this.loadAccountBTN);
            this.Controls.Add(this.emailTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vaultTB);
            this.Controls.Add(this.label2);
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
        private Label label2;
        private TextBox vaultTB;
        private Label label3;
        private TextBox emailTB;
        private Button loadAccountBTN;
        private DataGridViewTextBoxColumn AssignedTo;
        private DataGridViewTextBoxColumn PhoneNumber;
        private DataGridViewTextBoxColumn WhatsAppId;
        private Button createAccountBTN;
        private Button deleteAccountBTN;
        private Button updateAccountBTN;
    }
}