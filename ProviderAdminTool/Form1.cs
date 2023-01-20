using System.Runtime.InteropServices;
using System.Text.Json;
using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class Form1 : Form
    {
        readonly JSONSettings Settings = new();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        readonly static DataverseHandler dh = new();
        readonly SelectDynamicsEnvironment selectDynamicsEnvironment = new(dh);
        readonly string StartingPrefix = "";

        public Form1()
        {
            StartingPrefix = selectDynamicsEnvironment.InitializeWithPrefixReturn(dh);
            InitializeComponent();
            AllocConsole();
            byte[] filebytes = File.ReadAllBytes(Environment.CurrentDirectory + "/Settings.json");
#pragma warning disable CS8601
            Settings = JsonSerializer.Deserialize<JSONSettings>(filebytes);
#pragma warning restore CS8601
        }

        private void Formload(object sender, EventArgs e)
        {
            this.Hide();
        }

        void DisableAll()
        {
            loadAccountBTN.Enabled = false;
            createAccountBTN.Enabled = false;
            updateAccountBTN.Enabled = false;
            deleteAccountBTN.Enabled = false;
            vaultTB.Enabled = false;
            emailTB.Enabled = false;
            accountsDB.Enabled = false;
        }

        void EnableAll()
        {
            loadAccountBTN.Enabled = true;
            createAccountBTN.Enabled = true;
            updateAccountBTN.Enabled = true;
            deleteAccountBTN.Enabled = true;
            vaultTB.Enabled = true;
            emailTB.Enabled = true;
            accountsDB.Enabled = true;
        }

        public async void Button1_Click(object sender, EventArgs e)
        {
            DisableAll();
            if (vaultTB.Text != "")
            {
                accountsDB.Rows.Clear();
                dynamic profile;
                if (emailTB.Text == "")
                    profile = await dh.GetAccountsDBJSON(Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text);
                else
                    profile = await dh.GetAccountDBJSON(Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text, emailTB.Text);

                if (profile.value != null)
                {
                    for (int i = 0; i < profile.value.Count; i++)
                    {
                        accountsDB.Rows.Add(Globals.FindDynamicDataverseValue(profile, StartingPrefix + Settings.EmailAccountColumnName, i),
                            Globals.FindDynamicDataverseValue(profile, StartingPrefix + Settings.PhoneNumberColumnName, i),
                            Globals.FindDynamicDataverseValue(profile, StartingPrefix + Settings.PhoneNumberIDAccountColumnName, i));
                    }
                }
            }
            else
            {
                MessageBox.Show("Must have environment and internal keyvault name to pull account data.");
            }
            EnableAll();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DisableAll();
            AddNewUser addNewUser = new();
            addNewUser.SetupForm(this, vaultTB.Text, Settings, dh);
            this.Hide();
            addNewUser.ShowDialog();
            EnableAll();
        }

        private async void DeleteAccountBTN_Click(object sender, EventArgs e)
        {
            DisableAll();
            List<string> names = new();
            string fullmessage = "Please confirm, you are about to delete the following selected accounts:";
            for (int i = 0; i < accountsDB.SelectedRows.Count; i++)
            {
                names.Add(accountsDB.SelectedRows[i].Cells[0].Value.ToString());
                fullmessage += Environment.NewLine + accountsDB.SelectedRows[i].Cells[0].Value.ToString();
            }
            var results = MessageBox.Show(fullmessage, "Confirm Delete", MessageBoxButtons.OKCancel);
            if (results == DialogResult.OK)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    _ = await dh.DeleteAccountDB(Settings.PhoneNumberIDColumnName, Settings.EmailAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text, names[i]);
                }
                MessageBox.Show("Selected accounts have been deleted");
                Button1_Click(sender, e);
            }
            EnableAll();
        }

        private async void UpdateAccountBTN_Click(object sender, EventArgs e)
        {
            DisableAll();
            string fullmessage = "You are about to modify information for: ";
            for (int i = 0; i < accountsDB.SelectedRows.Count; i++)
            {
                fullmessage += Environment.NewLine + accountsDB.SelectedRows[i].Cells[0].Value.ToString();
            }
            var results = MessageBox.Show(fullmessage, "Confirm Update", MessageBoxButtons.OKCancel);
            if (results == DialogResult.OK)
            {
                NativeWindow nativeWindow = new();
                nativeWindow.AssignHandle(Handle);

                for (int i = 0; i < accountsDB.SelectedRows.Count; i++)
                {
                    Dictionary<string, object> profile = new()
                    {
                        { StartingPrefix + Settings.EmailAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                        { StartingPrefix + Settings.PhoneNumberColumnName, accountsDB.SelectedRows[i].Cells[1].Value.ToString() },
                        { StartingPrefix + Settings.PhoneNumberIDAccountColumnName, accountsDB.SelectedRows[i].Cells[2].Value.ToString() }
                    };
                    _ = await dh.PatchAccountDB(Settings.PhoneNumberIDColumnName, Settings.EmailAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text, profile[StartingPrefix + Settings.EmailAccountColumnName].ToString(), profile);
                    //Console.WriteLine(await PatchAccountDB(EmailAccountColumnName, DBAccountsSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName], profile));
                    Dictionary<string, object> profilesms = new()
                    {
                        { StartingPrefix + Settings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                        { StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[1].Value.ToString()?.Trim('+') }
                    };
                    _ = await dh.PatchSMSDB(nativeWindow, Settings.DBSMSSecretName, vaultTB.Text, profile[StartingPrefix + Settings.EmailAccountColumnName].ToString(), Settings.FromColumnName, Settings.ToColumnName, Settings.EmailNonAccountColumnName, profilesms);
                    //_ = await PatchSMSDB(Settings.FromColumnName, Settings.ToColumnName, Settings.EmailNonAccountColumnName, Settings.DBSMSSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + Settings.EmailAccountColumnName].ToString(), profilesms);
                    //var temp = await PatchSMSDB(FromColumnName, ToColumnName, EmailNonAccountColumnName, DBSMSSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName].ToString(), profilesms);
                    //for (int y = 0; y < temp.Count; y++)
                    //{
                    //Console.WriteLine(temp[y]);
                    //}
                    Dictionary<string, object> profilewhatsapp = new()
                    {
                        { StartingPrefix + Settings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                        { StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[2].Value.ToString() },
                    };
                    _ = await dh.PatchWhatsAppDB(nativeWindow, Settings.FromColumnName, Settings.ToColumnName, Settings.EmailNonAccountColumnName, Settings.DBWhatsAppSecretName, vaultTB.Text, profile[StartingPrefix + Settings.EmailAccountColumnName].ToString(), profilewhatsapp);
                    //var temp = await PatchWhatsAppDB(FromColumnName, ToColumnName, EmailNonAccountColumnName, DBWhatsAppSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName].ToString(), profilewhatsapp);
                    //for (int y = 0; y < temp.Count; y++)
                    //{
                        //Console.WriteLine(temp[y]);
                    //}
                }
                MessageBox.Show("Selected accounts have been updated");
                Button1_Click(sender, e);
            }
            EnableAll();
        }

        private void AccountsDB_EnableChanged(object sender, EventArgs e)
        {
            DataGridView? dgv = sender as DataGridView;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!dgv.Enabled)
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Control;
                dgv.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                //dgv.CurrentCell = null;
                dgv.ReadOnly = true;
                dgv.EnableHeadersVisualStyles = false;
            }
            else
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Window;
                dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ReadOnly = false;
                dgv.EnableHeadersVisualStyles = true;
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}