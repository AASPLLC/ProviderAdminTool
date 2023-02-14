namespace ProviderAdminTool
{
    partial class Profiles
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
            this.UpdateAccountBTN = new System.Windows.Forms.Button();
            this.LoadAccountsBTN = new System.Windows.Forms.Button();
            this.phonenumberTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.profilesDB = new System.Windows.Forms.DataGridView();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PicturePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.profilesDB)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateAccountBTN
            // 
            this.UpdateAccountBTN.Location = new System.Drawing.Point(421, 41);
            this.UpdateAccountBTN.Name = "UpdateAccountBTN";
            this.UpdateAccountBTN.Size = new System.Drawing.Size(156, 23);
            this.UpdateAccountBTN.TabIndex = 15;
            this.UpdateAccountBTN.Text = "Update Selected Account";
            this.UpdateAccountBTN.UseVisualStyleBackColor = true;
            this.UpdateAccountBTN.Click += new System.EventHandler(this.UpdateProfiles_Click);
            // 
            // LoadAccountsBTN
            // 
            this.LoadAccountsBTN.Location = new System.Drawing.Point(274, 41);
            this.LoadAccountsBTN.Name = "LoadAccountsBTN";
            this.LoadAccountsBTN.Size = new System.Drawing.Size(141, 23);
            this.LoadAccountsBTN.TabIndex = 14;
            this.LoadAccountsBTN.Text = "Refresh Account(s) List";
            this.LoadAccountsBTN.UseVisualStyleBackColor = true;
            this.LoadAccountsBTN.Click += new System.EventHandler(this.LoadProfiles_Click);
            // 
            // phonenumberTB
            // 
            this.phonenumberTB.Location = new System.Drawing.Point(162, 12);
            this.phonenumberTB.Name = "phonenumberTB";
            this.phonenumberTB.Size = new System.Drawing.Size(693, 23);
            this.phonenumberTB.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Phone Number To Lookup:";
            // 
            // profilesDB
            // 
            this.profilesDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.profilesDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PhoneNumber,
            this.PicturePath,
            this.DisplayName});
            this.profilesDB.Location = new System.Drawing.Point(12, 70);
            this.profilesDB.Name = "profilesDB";
            this.profilesDB.RowTemplate.Height = 25;
            this.profilesDB.Size = new System.Drawing.Size(843, 368);
            this.profilesDB.TabIndex = 11;
            this.profilesDB.EnabledChanged += new System.EventHandler(this.ProfilesDB_EnableChanged);
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.HeaderText = "Phone Number";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PhoneNumber.Width = 200;
            // 
            // PicturePath
            // 
            this.PicturePath.HeaderText = "Picture Path";
            this.PicturePath.Name = "PicturePath";
            this.PicturePath.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PicturePath.Width = 400;
            // 
            // DisplayName
            // 
            this.DisplayName.HeaderText = "Display Name";
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DisplayName.Width = 200;
            // 
            // Profiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 450);
            this.Controls.Add(this.UpdateAccountBTN);
            this.Controls.Add(this.LoadAccountsBTN);
            this.Controls.Add(this.phonenumberTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.profilesDB);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Profiles";
            this.Text = "Profiles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Load += new System.EventHandler(this.Formload);
            ((System.ComponentModel.ISupportInitialize)(this.profilesDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button UpdateAccountBTN;
        private Button LoadAccountsBTN;
        private TextBox phonenumberTB;
        private Label label3;
        private DataGridView profilesDB;
        private DataGridViewTextBoxColumn PhoneNumber;
        private DataGridViewTextBoxColumn PicturePath;
        private DataGridViewTextBoxColumn DisplayName;
    }
}