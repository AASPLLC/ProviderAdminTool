using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class Accounts : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int DBType = 0;
        public JSONDataverseSettings DataverseSettings = new();
        public JSONCosmosSettings CosmosSettings = new();

        readonly DataverseHandler dh = new();
        readonly CosmosDBHandler cosmos = new();
        public string vaultname = "";
        string cosmosRestSite = "";
        readonly List<string> oldnames = new();
        readonly SelectTool tool;

        public Accounts(SelectTool tool)
        {
            InitializeComponent();
            AllocConsole();
            DisableAll();
            this.tool = tool;
        }

        class JSONGlobals
        {
            public string? VaultName { get; set; }
            public string? DatabaseType { get; set; }
        }

        async void Formload(object sender, EventArgs e)
        {
            JSONGlobals globalsjson = await Globals.LoadJSON<JSONGlobals>(Environment.CurrentDirectory + "/Globals.json");
#pragma warning disable CS8601
            vaultname = globalsjson.VaultName;
            DBType = Convert.ToInt32(globalsjson.DatabaseType);

            if (DBType == 0)
            {
                DataverseSettings = await Globals.LoadJSON<JSONDataverseSettings>(Environment.CurrentDirectory + "/DataverseSettings.json");
                dh.SetCustomPrefix(await VaultHandler.GetSecretInteractive(vaultname, DataverseSettings.Environment), DataverseSettings.StartingPrefix);
            }
            else if (DBType == 1)
            {
                CosmosSettings = await Globals.LoadJSON<JSONCosmosSettings>(Environment.CurrentDirectory + "/CosmosSettings.json");
                //cosmosRestSite = "http://localhost:7250/api/Function1";
                cosmosRestSite = "https://" + await VaultHandler.GetSecretInteractive(vaultname, CosmosSettings.RestSiteSecretName) + ".azurewebsites.net/api/Function1";
            }
#pragma warning restore CS8601
            EnableAll();
            LoadAccounts_Click(sender, e);
        }

        void DisableAll()
        {
            LoadAccountsBTN.Enabled = false;
            UpdateAccountBTN.Enabled = false;
            DeleteAccountBTN.Enabled = false;
            aadTB.Enabled = false;
            accountsDB.Enabled = false;
        }

        void EnableAll()
        {
            LoadAccountsBTN.Enabled = true;
            UpdateAccountBTN.Enabled = true;
            DeleteAccountBTN.Enabled = true;
            aadTB.Enabled = true;
            accountsDB.Enabled = true;
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
                if (DBType == 0)
                {
                    for (int i = 0; i < names.Count; i++)
                    {
                        string?[] crosscheck = new[] { accountsDB.SelectedRows[i].Cells[2].Value.ToString(), accountsDB.SelectedRows[i].Cells[1].Value.ToString() };
#pragma warning disable CS8620
                        _ = await dh.DeleteAccountDB(DataverseSettings.AccountsDBEndingPrefix, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.PhoneNumberAccountColumnName, DataverseSettings.EmailAccountColumnName, names[i], crosscheck);
                        //Console.WriteLine(await dh.DeleteAccountDB(Settings.PhoneNumberIDColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.DBAccountsSecretName, vaultTB.Text, names[i], crosscheck));
#pragma warning restore CS8620
                    }
                }
                else
                {
                    if (accountsDB.SelectedRows.Count > 1)
                        MessageBox.Show("Only 1 account can be deleted at a time.");
                    else
                        Console.WriteLine(await CosmosDBHandler.DeleteAccount(cosmosRestSite, accountsDB.SelectedRows[0].Cells[0].Value.ToString()));
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
                        accountsDB.SelectedRows[i].Cells[1].Value ??= "";
                        accountsDB.SelectedRows[i].Cells[2].Value ??= "";
                        Dictionary<string, object> profile = new()
                        {
                            { DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberAccountColumnName, accountsDB.SelectedRows[i].Cells[1].Value.ToString() },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberIDAccountColumnName, accountsDB.SelectedRows[i].Cells[2].Value.ToString() }
                        };
                        string?[] crosscheck = new[] { accountsDB.SelectedRows[i].Cells[2].Value.ToString(), accountsDB.SelectedRows[i].Cells[1].Value.ToString() };
#pragma warning disable CS8620
                        string accountresponse = await dh.PatchAccountDB(DataverseSettings.AccountsDBEndingPrefix, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.PhoneNumberAccountColumnName, DataverseSettings.EmailAccountColumnName, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profile, crosscheck);
#pragma warning restore CS8620
                        if (accountresponse == "")
                        {
                            Dictionary<string, object> profilesms = new()
                            {
                                { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                                { DataverseSettings.StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[1].Value.ToString()?.Trim('+') }
                            };
                            List<string> smsresponses = await dh.PatchSMSDB(nativeWindow, DataverseSettings.SMSDBEndingPrefix, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, profilesms);
                            if (smsresponses[0] == "Cannot perform runtime binding on a null reference")
                                Console.WriteLine("No SMS messages found under: " + accountsDB.SelectedRows[i].Cells[0].Value.ToString());
                            Dictionary<string, object> profilewhatsapp = new()
                            {
                                { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, accountsDB.SelectedRows[i].Cells[0].Value.ToString() },
                                { DataverseSettings.StartingPrefix + "Number", accountsDB.SelectedRows[i].Cells[2].Value.ToString() },
                            };
                            List<string> whatsappresponse = await dh.PatchWhatsAppDB(nativeWindow, DataverseSettings.WhatsAppDBEndingPrefix, DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profilewhatsapp);
                            if (whatsappresponse[0] == "Cannot perform runtime binding on a null reference")
                                Console.WriteLine("No WhatsApp messages found under: " + accountsDB.SelectedRows[i].Cells[0].Value.ToString());
                            MessageBox.Show("Selected accounts have been updated");
                        }
                        else
                        {
                            DialogResult CanCreateAccount = MessageBox.Show("This account does not exist yet, would you like to create it?", "Account Not Found", MessageBoxButtons.YesNo);
                            if (CanCreateAccount == DialogResult.Yes)
                            {
#pragma warning disable CS8600
                                string account = accountsDB.SelectedRows[i].Cells[0].Value.ToString();
                                string smsNumber = accountsDB.SelectedRows[i].Cells[1].Value.ToString();
                                string whatsappNumber = accountsDB.SelectedRows[i].Cells[2].Value.ToString();
#pragma warning restore CS8600
                                if (Guid.TryParse(account, out _))
                                {
                                    Match match = Regex.Match(smsNumber, @"\d+");
                                    Match match2 = Regex.Match(whatsappNumber, @"\d+");
                                    if (match.Success && match2.Success)
                                    {
                                        if (!smsNumber.StartsWith("+"))
                                            smsNumber = "+" + smsNumber;

                                        await dh.CreateAccountDB(
                                            await VaultHandler.GetSecretInteractive(vaultname, DataverseSettings.ClientIDSecretName),
                                            DataverseSettings.AccountsDBEndingPrefix,
                                            DataverseSettings.PhoneNumberAccountColumnName,
                                            DataverseSettings.EmailAccountColumnName,
                                            DataverseSettings.PhoneNumberIDAccountColumnName,
                                            account,
                                            smsNumber,
                                            whatsappNumber);
                                    }
                                    else
                                        MessageBox.Show("SMS Phone Number & WhatsApp Phone Number ID can only contain numbers.");
                                }
                                else
                                    MessageBox.Show("AAD Object ID must be a valid GUID.");
                            }
                        }
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
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                        string[] DBColumnNames = new[]
                        {
                                DataverseSettings.EmailAccountColumnName,
                                DataverseSettings.PhoneNumberAccountColumnName,
                                DataverseSettings.PhoneNumberIDAccountColumnName
                        };
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                        dynamic profile;
                        if (aadTB.Text == "")
                        {
                            string _results = await dh.GetAllDBInfoNoFilterJSON(DBColumnNames, DataverseSettings.AccountsDBEndingPrefix);
                            profile = Globals.DynamicJsonDeserializer(_results);
                        }
                        else
                        {
                            if (Guid.TryParse(aadTB.Text, out _))
                            {
                                string _results = await dh.GetAllDBInfoFilteredJSON(DBColumnNames, 0, aadTB.Text.Trim(), DataverseSettings.AccountsDBEndingPrefix);
                                profile = Globals.DynamicJsonDeserializer(_results);
                            }
                            else
                            {
                                MessageBox.Show("AAD Object ID is not a valid GUID format.");
                                return;
                            }
                        }

                        if (profile.value != null)
                        {
                            for (int i = 0; i < profile.value.Count; i++)
                            {
                                accountsDB.Rows.Add(Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName, i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberAccountColumnName, i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberIDAccountColumnName, i));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    oldnames.Clear();
                    List<CosmosDBHandler.JSONAdminAccountResponse> adminResponses = await CosmosDBHandler.GetAllAccounts(cosmosRestSite);
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

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            tool.Close();
        }
    }
}