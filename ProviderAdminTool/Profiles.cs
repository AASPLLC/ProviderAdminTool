using AASPGlobalLibrary;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ProviderAdminTool
{
    public partial class Profiles : Form
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
        readonly List<string> oldnumbers = new();
        readonly SelectTool tool;

        public Profiles(SelectTool tool)
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
            LoadProfiles_Click(sender, e);
        }

        void DisableAll()
        {
            LoadAccountsBTN.Enabled = false;
            UpdateAccountBTN.Enabled = false;
            phonenumberTB.Enabled = false;
            profilesDB.Enabled = false;
        }

        void EnableAll()
        {
            LoadAccountsBTN.Enabled = true;
            UpdateAccountBTN.Enabled = true;
            phonenumberTB.Enabled = true;
            profilesDB.Enabled = true;
        }

        private async void LoadProfiles_Click(object sender, EventArgs e)
        {
            DisableAll();
            if (vaultname != "")
            {
                profilesDB.Rows.Clear();
                if (DBType == 0)
                {
                    try
                    {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                        string[] DBColumnNames = new[]
                        {
                                DataverseSettings.PhoneNumberProfileColumnName,
                                DataverseSettings.PicturePathProfileColumnName,
                                DataverseSettings.DisplayNameProfileColumnName
                        };
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                        dynamic profile;
                        if (phonenumberTB.Text == "")
                        {
                            string _results = await dh.GetAllDBInfoNoFilterJSON(DBColumnNames, DataverseSettings.PhoneNumbersDBEndingPrefix);
                            profile = Globals.DynamicJsonDeserializer(_results);
                        }
                        else
                        {
                            Match match = Regex.Match(phonenumberTB.Text, @"\d+");
                            if (match.Success)
                            {
                                phonenumberTB.Text = phonenumberTB.Text.Trim('+').Trim();
                                string _results = await dh.GetAllDBInfoFilteredJSON(DBColumnNames, 0, phonenumberTB.Text.Trim(), DataverseSettings.PhoneNumbersDBEndingPrefix);
                                profile = Globals.DynamicJsonDeserializer(_results);
                            }
                            else
                            {
                                MessageBox.Show("The Phone Number can only contain numbers.");
                                return;
                            }
                        }

                        if (profile.value != null)
                        {
                            for (int i = 0; i < profile.value.Count; i++)
                            {
                                profilesDB.Rows.Add(Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DBColumnNames[0], i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DBColumnNames[1], i),
                                    Globals.FindDynamicDataverseValue(profile, DataverseSettings.StartingPrefix + DBColumnNames[2], i));
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
                    List<CosmosDBHandler.JSONAdminProfileResponse> adminResponses = await CosmosDBHandler.GetAllProfiles(cosmosRestSite);
                    oldnumbers.Clear();
                    for (int i = 0; i < adminResponses.Count; i++)
                    {
                        oldnumbers.Add(adminResponses[i].PhoneNumber);
                        profilesDB.Rows.Add(adminResponses[i].PhoneNumber, adminResponses[i].PicturePath, adminResponses[i].DisplayName);
                    }
                }
            }
            else
            {
                MessageBox.Show("An environment and internal keyvault name is required to pull account data.");
            }
            EnableAll();
        }

        private void ProfilesDB_EnableChanged(object sender, EventArgs e)
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

        private async void UpdateProfiles_Click(object sender, EventArgs e)
        {
            DisableAll();
            if (vaultname != "")
            {
                string fullmessage = "You are about to modify information for: ";
                for (int i = 0; i < profilesDB.SelectedRows.Count; i++)
                {
                    fullmessage += Environment.NewLine + profilesDB.SelectedRows[i].Cells[0].Value.ToString();
                }
                var results = MessageBox.Show(fullmessage, "Confirm Update", MessageBoxButtons.OKCancel);
                if (results == DialogResult.OK)
                {
                    NativeWindow nativeWindow = new();
                    nativeWindow.AssignHandle(Handle);

                    if (DBType == 0)
                    {
                        for (int i = 0; i < profilesDB.SelectedRows.Count; i++)
                        {
                            profilesDB.SelectedRows[i].Cells[1].Value ??= "";
                            profilesDB.SelectedRows[i].Cells[2].Value ??= "";
                            Dictionary<string, object> profile = new()
                            {
                                { DataverseSettings.StartingPrefix + DataverseSettings.PhoneNumberProfileColumnName, profilesDB.SelectedRows[i].Cells[0].Value.ToString() },
                                { DataverseSettings.StartingPrefix + DataverseSettings.PicturePathProfileColumnName, profilesDB.SelectedRows[i].Cells[1].Value.ToString() },
                                { DataverseSettings.StartingPrefix + DataverseSettings.DisplayNameProfileColumnName, profilesDB.SelectedRows[i].Cells[2].Value.ToString() }
                            };
                            string accountresponse = await dh.PatchAccountDB(DataverseSettings.PhoneNumbersDBEndingPrefix, DataverseSettings.PhoneNumberProfileColumnName, profilesDB.SelectedRows[i].Cells[0].Value.ToString(), profile);
                            if (accountresponse != "")
                            {
                                DialogResult CanCreateAccount = MessageBox.Show("This phone number does not exist yet, would you like to create it?", "Account Not Found", MessageBoxButtons.YesNo);
                                if (CanCreateAccount == DialogResult.Yes)
                                {
#pragma warning disable CS8600
                                    string phonenumber = profilesDB.SelectedRows[i].Cells[0].Value.ToString();
                                    string picturePath = profilesDB.SelectedRows[i].Cells[1].Value.ToString();
                                    string displayName = profilesDB.SelectedRows[i].Cells[2].Value.ToString();
#pragma warning restore CS8600
                                    Match match = Regex.Match(phonenumber, @"\d+");
                                    if (match.Success)
                                    {
                                        phonenumber = phonenumber.Trim('+').Trim();

                                        await dh.CreateAccountDB(
                                            await VaultHandler.GetSecretInteractive(vaultname, DataverseSettings.ClientIDSecretName),
                                            DataverseSettings.PhoneNumbersDBEndingPrefix,
                                            DataverseSettings.PicturePathProfileColumnName,
                                            DataverseSettings.PhoneNumberProfileColumnName,
                                            DataverseSettings.DisplayNameProfileColumnName,
                                            phonenumber,
                                            picturePath,
                                            displayName);
                                    }
                                    else
                                        MessageBox.Show("The Phone Number can only contain numbers.");
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < profilesDB.SelectedRows.Count; i++)
                        {
                            profilesDB.SelectedRows[i].Cells[1].Value ??= "";
                            profilesDB.SelectedRows[i].Cells[2].Value ??= "";
#pragma warning disable CS8600
                            string phonenumber = profilesDB.SelectedRows[i].Cells[0].Value.ToString();
                            string picturePath = profilesDB.SelectedRows[i].Cells[1].Value.ToString();
                            string displayName = profilesDB.SelectedRows[i].Cells[2].Value.ToString();
#pragma warning restore CS8600
                            Match match = Regex.Match(phonenumber, @"\d+");
                            if (match.Success)
                            {
                                phonenumber = phonenumber.Trim('+').Trim();

                                if (oldnumbers.Contains(phonenumber))
                                {
                                    Console.WriteLine(await CosmosDBHandler.UpdateProfile(cosmosRestSite,
                                    phonenumber,
                                    picturePath,
                                    displayName));
                                }
                                else
                                {
                                    Console.WriteLine(await CosmosDBHandler.AddProfile(cosmosRestSite,
                                    phonenumber,
                                    picturePath,
                                    displayName));
                                }
                            }
                            else
                                MessageBox.Show("The Phone Number can only contain numbers.");
                        }
                    }
                    LoadProfiles_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("An environment and internal keyvault name is required to pull account data.");
            }
            EnableAll();
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            tool.Close();
        }
    }
}
