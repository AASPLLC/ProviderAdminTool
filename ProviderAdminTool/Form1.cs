using System.Runtime.InteropServices;
using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class Form1 : Form
    {
        int DBType = 0;
        public JSONDataverseSettings DataverseSettings = new();
        readonly JSONCosmosSettings CosmosSettings = new();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        readonly DataverseHandler dh = new();
        readonly CosmosDBHandler cosmos = new();
        public string vaultname = "";
        string cosmosRestSite = "";
        readonly List<string> oldnames = new();

        public Form1()
        {
            InitializeComponent();
            AllocConsole();
        }

        public void Init(int dbtype, string environment)
        {
            DBType = dbtype;
            CreateAccountBTN.Enabled = true;
            dh.SetCustomPrefix(environment, DataverseSettings.StartingPrefix);
        }

        public async void Init(int dbtype)
        {
            DBType = dbtype;

            dynamic? globalsjson = await Globals.LoadJSONDynamic(Environment.CurrentDirectory + "/Globals.json");
#pragma warning disable CS8601
            vaultname = globalsjson?.VaultName;

            CreateAccountBTN.Enabled = false;
            cosmosRestSite = "http://localhost:7250/api/Function1";
            //cosmosRestSite = "https://" + await VaultHandler.GetSecretInteractive(vaultname, CosmosSettings.RestSiteSecretName) + ".azurewebsites.net";
            DataverseSettings = await Globals.LoadJSON<JSONDataverseSettings>(Environment.CurrentDirectory + "/CosmosSettings.json");
#pragma warning restore CS8601
        }

        private void Formload(object sender, EventArgs e)
        {
            ChooseDBType dBType = new(this);
            this.Hide();
            dBType.ShowDialog();
        }

        void DisableAll()
        {
            LoadAccountsBTN.Enabled = false;
            CreateAccountBTN.Enabled = false;
            UpdateAccountBTN.Enabled = false;
            DeleteAccountBTN.Enabled = false;
            emailTB.Enabled = false;
            accountsDB.Enabled = false;
        }

        void EnableAll()
        {
            LoadAccountsBTN.Enabled = true;
            CreateAccountBTN.Enabled = true;
            UpdateAccountBTN.Enabled = true;
            DeleteAccountBTN.Enabled = true;
            emailTB.Enabled = true;
            accountsDB.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DisableAll();
            AddNewUser addNewUser = new(this, vaultname, DataverseSettings, dh);
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
                    string?[] crosscheck = new[] { accountsDB.SelectedRows[i].Cells[2].Value.ToString(), accountsDB.SelectedRows[i].Cells[1].Value.ToString() };
#pragma warning disable CS8620
                    _ = await dh.DeleteAccountDB(DataverseSettings.PhoneNumberIDColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.PhoneNumberColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.DBAccountsSecretName, vaultname, names[i], crosscheck);
                    //Console.WriteLine(await dh.DeleteAccountDB(Settings.PhoneNumberIDColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text, names[i], crosscheck));
#pragma warning restore CS8620
                }
                MessageBox.Show("Selected accounts have been deleted");
                LoadAccounts_Click(sender, e);
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

                if (DBType == 0)
                {
                    for (int i = 0; i < accountsDB.SelectedRows.Count; i++)
                    {
                        Dictionary<string, object> profile = new()
                        {
                            { DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberColumnName, accountsDB.SelectedRows[i].Cells[1].Value.ToString() },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberIDAccountColumnName, accountsDB.SelectedRows[i].Cells[2].Value.ToString() }
                        };
                        string?[] crosscheck = new[] { accountsDB.SelectedRows[i].Cells[2].Value.ToString(), accountsDB.SelectedRows[i].Cells[1].Value.ToString() };
#pragma warning disable CS8620
                        _ = await dh.PatchAccountDB(DataverseSettings.PhoneNumberIDColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.PhoneNumberColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.DBAccountsSecretName, vaultname, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profile, crosscheck);
#pragma warning restore CS8620
                        //Console.WriteLine(await PatchAccountDB(EmailAccountColumnName, DBAccountsSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName], profile));
                        Dictionary<string, object> profilesms = new()
                        {
                            { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                            { DataverseSettings.StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[1].Value.ToString()?.Trim('+') }
                        };
                        _ = await dh.PatchSMSDB(nativeWindow, DataverseSettings.DBSMSSecretName, vaultname, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, profilesms);
                        //_ = await PatchSMSDB(Settings.FromColumnName, Settings.ToColumnName, Settings.EmailNonAccountColumnName, Settings.DBSMSSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + Settings.EmailAccountColumnName].ToString(), profilesms);
                        //var temp = await PatchSMSDB(FromColumnName, ToColumnName, EmailNonAccountColumnName, DBSMSSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName].ToString(), profilesms);
                        //for (int y = 0; y < temp.Count; y++)
                        //{
                        //Console.WriteLine(temp[y]);
                        //}
                        Dictionary<string, object> profilewhatsapp = new()
                        {
                            { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                            { DataverseSettings.StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[2].Value.ToString() },
                        };
                        _ = await dh.PatchWhatsAppDB(nativeWindow, DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, DataverseSettings.DBWhatsAppSecretName, vaultname, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profilewhatsapp);
                        //var temp = await PatchWhatsAppDB(FromColumnName, ToColumnName, EmailNonAccountColumnName, DBWhatsAppSecretName, vaultTB.Text, environmentTB.Text, profile[StartingPrefix + EmailAccountColumnName].ToString(), profilewhatsapp);
                        //for (int y = 0; y < temp.Count; y++)
                        //{
                        //Console.WriteLine(temp[y]);
                        //}
                    }
                }
                else
                {
                    for (int i = 0; i < accountsDB.SelectedRows.Count; i++)
                    {
                        Console.WriteLine(oldnames[accountsDB.SelectedRows[i].Index]);
                        Console.WriteLine(await CosmosDBHandler.AddOrUpdateAccount(cosmosRestSite,
                            oldnames[accountsDB.SelectedRows[i].Index],
                            accountsDB.SelectedRows[i].Cells[0].Value.ToString(),
                            accountsDB.SelectedRows[i].Cells[1].Value.ToString(),
                            accountsDB.SelectedRows[i].Cells[2].Value.ToString(),
                            accountsDB.SelectedRows[i].Cells[3].Value.ToString()));
                        Console.WriteLine(await CosmosDBHandler.UpdateSMSAndWhatsAppAssignedUser(cosmosRestSite,
                            oldnames[accountsDB.SelectedRows[i].Index],
                            accountsDB.SelectedRows[i].Cells[0].Value.ToString()));
                    }
                }
                MessageBox.Show("Selected accounts have been updated");
                LoadAccounts_Click(sender, e);
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

        public async void LoadAccounts_Click(object sender, EventArgs e)
        {
            DisableAll();
            if (vaultname != "")
            {
                accountsDB.Rows.Clear();
                if (DBType == 0)
                {
                    try
                    {
                        dynamic profile;
                        if (emailTB.Text == "")
                            profile = Globals.DynamicJsonDeserializer(await dh.GetAccountsDBJSON(DataverseSettings.PhoneNumberColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.DBAccountsSecretName, vaultname));
                        else
                            profile = Globals.DynamicJsonDeserializer(await dh.GetAccountDBJSON(DataverseSettings.PhoneNumberColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.DBAccountsSecretName, vaultname, emailTB.Text.Trim()));

                        if (profile.value != null)
                        {
                            for (int i = 0; i < profile.value.Count; i++)
                            {
                                accountsDB.Rows.Add(Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName, i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberColumnName, i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberIDAccountColumnName, i));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(await dh.GetAccountsDBJSON(DataverseSettings.PhoneNumberColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.DBAccountsSecretName, vaultname) as string);
                    }
                }
                else
                {
                    oldnames.Clear();
                    List<CosmosDBHandler.JSONAdminResponse> adminResponses = await CosmosDBHandler.GetAllAccounts(cosmosRestSite);
                    for (int i = 0; i < adminResponses.Count; i++)
                    {
                        oldnames.Add(adminResponses[i].AssignedTo);
                        accountsDB.Rows.Add(adminResponses[i].AssignedTo, adminResponses[i].PhoneNumber, adminResponses[i].PhoneNumberID, adminResponses[i].RoleID);
                    }
                }
            }
            else
            {
                MessageBox.Show("Must have environment and internal keyvault name to pull account data.");
            }
            EnableAll();
        }
    }
}