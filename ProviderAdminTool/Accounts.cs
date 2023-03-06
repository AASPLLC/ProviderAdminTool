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
        public string internalVaultName = "";
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

        async void Formload(object sender, EventArgs e)
        {
            JSONGlobals globalsjson = await Globals.LoadJSON<JSONGlobals>(Environment.CurrentDirectory + "/Globals.json");
#pragma warning disable CS8601
            internalVaultName = globalsjson.InternalVaultName;
            DBType = Convert.ToInt32(globalsjson.DatabaseType);

            if (DBType == 0)
            {
                DataverseSettings = await Globals.LoadJSON<JSONDataverseSettings>(Environment.CurrentDirectory + "/DataverseSettings.json");
                dh.SetCustomPrefix(await VaultHandler.GetSecretInteractive(globalsjson.PublicVaultName, DataverseSettings.Environment), DataverseSettings.StartingPrefix);
            }
            else if (DBType == 1)
            {
                CosmosSettings = await Globals.LoadJSON<JSONCosmosSettings>(Environment.CurrentDirectory + "/CosmosSettings.json");
                //cosmosRestSite = "http://localhost:7250/api/Function1";
                cosmosRestSite = "https://" + await VaultHandler.GetSecretInteractive(globalsjson.PublicVaultName, CosmosSettings.RestSiteSecretName) + ".azurewebsites.net/api/Function1";
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
            bool FoundNull = false;
            foreach (DataGridViewTextBoxCell item in accountsDB.SelectedCells)
            {
                item.OwningRow.Selected = true;
                //the -1 is to skip the cosmos column
                int count = item.OwningRow.Cells.Count;
                if (DBType == 0) count -= 1;
                for (int i = 0; i < count; i++)
                {
                    if (string.IsNullOrEmpty(item.OwningRow.Cells[i].Value.ToString()))
                    {
                        FoundNull = true;
                        break;
                    }
                }
            }
            if (!FoundNull)
            {
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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Dereference of a possibly null reference.
                            string AccountID = accountsDB.SelectedRows[i].Cells[0].Value.ToString();
                            string PhoneNumber = accountsDB.SelectedRows[i].Cells[1].Value.ToString();
                            string PhoneNumberID = accountsDB.SelectedRows[i].Cells[2].Value.ToString();
                            AccountID = AccountID.Replace(" ", "");
                            PhoneNumber = PhoneNumber.Replace(" ", "");
                            PhoneNumberID = PhoneNumberID.Replace(" ", "");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Dereference of a possibly null reference.
                            Dictionary<string, object> profile = new()
                        {
                            { DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName, AccountID },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberAccountColumnName, PhoneNumber },
                            { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberIDAccountColumnName, PhoneNumberID }
                        };
                            string?[] crosscheck = new[] { PhoneNumberID, PhoneNumber };
#pragma warning disable CS8620
                            string accountresponse = await dh.PatchAccountDB(DataverseSettings.AccountsDBEndingPrefix, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.PhoneNumberAccountColumnName, DataverseSettings.EmailAccountColumnName, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profile, crosscheck);
#pragma warning restore CS8620
                            if (accountresponse == "")
                            {
                                Dictionary<string, object> profilesms = new()
                            {
                                { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, AccountID },
                                { DataverseSettings.StartingPrefix + "Number", PhoneNumber?.Trim('+') }
                            };
                                List<string> smsresponses = await dh.PatchSMSDB(nativeWindow, DataverseSettings.SMSDBEndingPrefix, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, profilesms);
                                if (smsresponses[0] == "Cannot perform runtime binding on a null reference")
                                    Console.WriteLine("No SMS messages found under: " + AccountID);
                                Dictionary<string, object> profilewhatsapp = new()
                            {
                                { DataverseSettings.StartingPrefix + DataverseSettings.EmailNonAccountColumnName, AccountID },
                                { DataverseSettings.StartingPrefix + "Number", PhoneNumberID },
                            };
                                List<string> whatsappresponse = await dh.PatchWhatsAppDB(nativeWindow, DataverseSettings.WhatsAppDBEndingPrefix, DataverseSettings.FromColumnName, DataverseSettings.ToColumnName, DataverseSettings.EmailNonAccountColumnName, profile[DataverseSettings.StartingPrefix + DataverseSettings.EmailAccountColumnName].ToString(), profilewhatsapp);
                                if (whatsappresponse[0] == "Cannot perform runtime binding on a null reference")
                                    Console.WriteLine("No WhatsApp messages found under: " + AccountID);
                                MessageBox.Show("Selected accounts have been updated");
                            }
                            else
                            {
                                DialogResult CanCreateAccount = MessageBox.Show("This account does not exist yet, would you like to create it?", "Account Not Found", MessageBoxButtons.YesNo);
                                if (CanCreateAccount == DialogResult.Yes)
                                {
                                    if (Guid.TryParse(AccountID, out _))
                                    {
                                        Match match = Regex.Match(PhoneNumber, @"\d+");
                                        Match match2 = Regex.Match(PhoneNumberID, @"\d+");
                                        if (match.Success && match2.Success)
                                        {
                                            if (!PhoneNumber.StartsWith("+"))
                                                PhoneNumber = "+" + PhoneNumber;

                                            await dh.CreateAccountDB(
                                                await VaultHandler.GetSecretInteractive(internalVaultName, DataverseSettings.ClientIDSecretName),
                                                DataverseSettings.AccountsDBEndingPrefix,
                                                DataverseSettings.PhoneNumberAccountColumnName,
                                                DataverseSettings.EmailAccountColumnName,
                                                DataverseSettings.PhoneNumberIDAccountColumnName,
                                                AccountID,
                                                PhoneNumber,
                                                PhoneNumberID);
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
                            accountsDB.SelectedRows[i].Cells[1].Value ??= "";
                            accountsDB.SelectedRows[i].Cells[2].Value ??= "";
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Dereference of a possibly null reference.
                            string AccountID = accountsDB.SelectedRows[i].Cells[0].Value.ToString();
                            string PhoneNumber = accountsDB.SelectedRows[i].Cells[1].Value.ToString();
                            string PhoneNumberID = accountsDB.SelectedRows[i].Cells[2].Value.ToString();
                            AccountID = AccountID.Replace(" ", "");
                            PhoneNumber = PhoneNumber.Replace(" ", "");
                            PhoneNumberID = PhoneNumberID.Replace(" ", "");
                            Console.WriteLine(oldnames[accountsDB.SelectedRows[i].Index]);
                            Console.WriteLine(await CosmosDBHandler.AddOrUpdateAccount(cosmosRestSite,
                                oldnames[accountsDB.SelectedRows[i].Index],
                                AccountID,
                                PhoneNumber,
                                PhoneNumberID,
                                accountsDB.SelectedRows[i].Cells[3].Value.ToString().Trim()));
                            Console.WriteLine(await CosmosDBHandler.UpdateSMSAndWhatsAppAssignedUser(cosmosRestSite,
                                oldnames[accountsDB.SelectedRows[i].Index],
                                AccountID));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Dereference of a possibly null reference.
                        }
                    }
                    LoadAccounts_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Empty fields found, cannot create.");
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
            if (internalVaultName != "")
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