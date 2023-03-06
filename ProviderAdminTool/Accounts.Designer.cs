namespace ProviderAdminTool
{
    partial class Accounts
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
            accountsDB = new DataGridView();
            AssignedTo = new DataGridViewTextBoxColumn();
            PhoneNumber = new DataGridViewTextBoxColumn();
            WhatsAppId = new DataGridViewTextBoxColumn();
            RoleID = new DataGridViewTextBoxColumn();
            label3 = new Label();
            aadTB = new TextBox();
            LoadAccountsBTN = new Button();
            DeleteAccountBTN = new Button();
            UpdateAccountBTN = new Button();
            ((System.ComponentModel.ISupportInitialize)accountsDB).BeginInit();
            SuspendLayout();
            // 
            // accountsDB
            // 
            accountsDB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            accountsDB.Columns.AddRange(new DataGridViewColumn[] { AssignedTo, PhoneNumber, WhatsAppId, RoleID });
            accountsDB.Location = new Point(12, 70);
            accountsDB.Name = "accountsDB";
            accountsDB.RowTemplate.Height = 25;
            accountsDB.Size = new Size(843, 368);
            accountsDB.TabIndex = 0;
            accountsDB.EnabledChanged += AccountsDB_EnableChanged;
            // 
            // AssignedTo
            // 
            AssignedTo.HeaderText = "Assigned To:";
            AssignedTo.Name = "AssignedTo";
            AssignedTo.Resizable = DataGridViewTriState.False;
            AssignedTo.Width = 300;
            // 
            // PhoneNumber
            // 
            PhoneNumber.HeaderText = "SMS Phone Number:";
            PhoneNumber.Name = "PhoneNumber";
            PhoneNumber.Resizable = DataGridViewTriState.False;
            PhoneNumber.Width = 200;
            // 
            // WhatsAppId
            // 
            WhatsAppId.HeaderText = "WhatsApp Phone Number ID:";
            WhatsAppId.Name = "WhatsAppId";
            WhatsAppId.Resizable = DataGridViewTriState.False;
            WhatsAppId.Width = 200;
            // 
            // RoleID
            // 
            RoleID.HeaderText = "Role ID:";
            RoleID.Name = "RoleID";
            RoleID.Resizable = DataGridViewTriState.False;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 15);
            label3.Name = "label3";
            label3.Size = new Size(144, 15);
            label3.TabIndex = 5;
            label3.Text = "AAD Object ID To Lookup:";
            // 
            // aadTB
            // 
            aadTB.Location = new Point(162, 12);
            aadTB.Name = "aadTB";
            aadTB.Size = new Size(693, 23);
            aadTB.TabIndex = 6;
            // 
            // LoadAccountsBTN
            // 
            LoadAccountsBTN.Location = new Point(214, 41);
            LoadAccountsBTN.Name = "LoadAccountsBTN";
            LoadAccountsBTN.Size = new Size(141, 23);
            LoadAccountsBTN.TabIndex = 7;
            LoadAccountsBTN.Text = "Refresh Account(s) List";
            LoadAccountsBTN.UseVisualStyleBackColor = true;
            LoadAccountsBTN.Click += LoadAccounts_Click;
            // 
            // DeleteAccountBTN
            // 
            DeleteAccountBTN.Location = new Point(523, 41);
            DeleteAccountBTN.Name = "DeleteAccountBTN";
            DeleteAccountBTN.Size = new Size(145, 23);
            DeleteAccountBTN.TabIndex = 9;
            DeleteAccountBTN.Text = "Delete Selected Account";
            DeleteAccountBTN.UseVisualStyleBackColor = true;
            DeleteAccountBTN.Click += DeleteAccountBTN_Click;
            // 
            // UpdateAccountBTN
            // 
            UpdateAccountBTN.Location = new Point(361, 41);
            UpdateAccountBTN.Name = "UpdateAccountBTN";
            UpdateAccountBTN.Size = new Size(156, 23);
            UpdateAccountBTN.TabIndex = 10;
            UpdateAccountBTN.Text = "Update Selected Account";
            UpdateAccountBTN.UseVisualStyleBackColor = true;
            UpdateAccountBTN.Click += UpdateAccountBTN_Click;
            // 
            // Accounts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 450);
            Controls.Add(UpdateAccountBTN);
            Controls.Add(DeleteAccountBTN);
            Controls.Add(LoadAccountsBTN);
            Controls.Add(aadTB);
            Controls.Add(label3);
            Controls.Add(accountsDB);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Accounts";
            Text = "Accounts";
            FormClosed += Form_Closed;
            Load += Formload;
            ((System.ComponentModel.ISupportInitialize)accountsDB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView accountsDB;
        private Label label3;
        private TextBox aadTB;
        private Button LoadAccountsBTN;
        private Button DeleteAccountBTN;
        private Button UpdateAccountBTN;
        private DataGridViewTextBoxColumn AssignedTo;
        private DataGridViewTextBoxColumn PhoneNumber;
        private DataGridViewTextBoxColumn WhatsAppId;
        private DataGridViewTextBoxColumn RoleID;
    }
}