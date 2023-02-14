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
            this.accountsDB = new System.Windows.Forms.DataGridView();
            this.AssignedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhatsAppId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.aadTB = new System.Windows.Forms.TextBox();
            this.LoadAccountsBTN = new System.Windows.Forms.Button();
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
            this.AssignedTo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AssignedTo.Width = 300;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.HeaderText = "SMS Phone Number:";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PhoneNumber.Width = 200;
            // 
            // WhatsAppId
            // 
            this.WhatsAppId.HeaderText = "WhatsApp Phone Number ID:";
            this.WhatsAppId.Name = "WhatsAppId";
            this.WhatsAppId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.WhatsAppId.Width = 200;
            // 
            // RoleID
            // 
            this.RoleID.HeaderText = "Role ID:";
            this.RoleID.Name = "RoleID";
            this.RoleID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "AAD Object ID To Lookup:";
            // 
            // aadTB
            // 
            this.aadTB.Location = new System.Drawing.Point(162, 12);
            this.aadTB.Name = "aadTB";
            this.aadTB.Size = new System.Drawing.Size(693, 23);
            this.aadTB.TabIndex = 6;
            // 
            // LoadAccountsBTN
            // 
            this.LoadAccountsBTN.Location = new System.Drawing.Point(214, 41);
            this.LoadAccountsBTN.Name = "LoadAccountsBTN";
            this.LoadAccountsBTN.Size = new System.Drawing.Size(141, 23);
            this.LoadAccountsBTN.TabIndex = 7;
            this.LoadAccountsBTN.Text = "Refresh Account(s) List";
            this.LoadAccountsBTN.UseVisualStyleBackColor = true;
            this.LoadAccountsBTN.Click += new System.EventHandler(this.LoadAccounts_Click);
            // 
            // DeleteAccountBTN
            // 
            this.DeleteAccountBTN.Location = new System.Drawing.Point(523, 41);
            this.DeleteAccountBTN.Name = "DeleteAccountBTN";
            this.DeleteAccountBTN.Size = new System.Drawing.Size(145, 23);
            this.DeleteAccountBTN.TabIndex = 9;
            this.DeleteAccountBTN.Text = "Delete Selected Account";
            this.DeleteAccountBTN.UseVisualStyleBackColor = true;
            this.DeleteAccountBTN.Click += new System.EventHandler(this.DeleteAccountBTN_Click);
            // 
            // UpdateAccountBTN
            // 
            this.UpdateAccountBTN.Location = new System.Drawing.Point(361, 41);
            this.UpdateAccountBTN.Name = "UpdateAccountBTN";
            this.UpdateAccountBTN.Size = new System.Drawing.Size(156, 23);
            this.UpdateAccountBTN.TabIndex = 10;
            this.UpdateAccountBTN.Text = "Update Selected Account";
            this.UpdateAccountBTN.UseVisualStyleBackColor = true;
            this.UpdateAccountBTN.Click += new System.EventHandler(this.UpdateAccountBTN_Click);
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 450);
            this.Controls.Add(this.UpdateAccountBTN);
            this.Controls.Add(this.DeleteAccountBTN);
            this.Controls.Add(this.LoadAccountsBTN);
            this.Controls.Add(this.aadTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountsDB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Accounts";
            this.Text = "Accounts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Load += new System.EventHandler(this.Formload);
            ((System.ComponentModel.ISupportInitialize)(this.accountsDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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