using System.Text.RegularExpressions;
using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class AddNewUser : Form
    {
        readonly Form1 form;
        readonly string keyvault;
        readonly JSONDataverseSettings DataverseSettings;
        readonly DataverseHandler dh;

        public AddNewUser(Form1 form, string keyvault, JSONDataverseSettings Settings, DataverseHandler dh)
        {
            InitializeComponent();
            this.form = form;
            this.keyvault = keyvault.Trim();
            this.DataverseSettings = Settings;
            this.dh = dh;
        }
        
        private async void Button1_Click(object sender, EventArgs e)
        {
            if (emailTB.Text.Contains('@') && emailTB.Text.Contains('.'))
            {
                Match match = Regex.Match(smsNumberTB.Text, @"\d+");
                Match match2 = Regex.Match(whatsappIDTB.Text, @"\d+");
                if (match.Success && match2.Success)
                {
                    if (!smsNumberTB.Text.StartsWith("+"))
                        smsNumberTB.Text = "+" + smsNumberTB.Text;
                    button1.Enabled = false;
                    emailTB.Enabled = false;
                    smsNumberTB.Enabled = false;
                    whatsappIDTB.Enabled = false;

                    await dh.CreateAccountDB(DataverseSettings.ClientIDSecretName, DataverseSettings.PhoneNumberAccountColumnName, DataverseSettings.EmailAccountColumnName, DataverseSettings.PhoneNumberIDAccountColumnName, DataverseSettings.DBAccountsSecretName, keyvault, emailTB.Text, smsNumberTB.Text, whatsappIDTB.Text);
                    //await DataverseHandler.CreateAccountDBSecret(Settings.ClientIDSecretName, Settings.ClientSecretSecretName, Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.DBAccountsSecretName, keyvault, environment, emailTB.Text, smsNumberTB.Text, whatsappIDTB.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("SMS Phone Number & WhatsApp Phone Number ID can only contain numbers.");
            }
            else
                MessageBox.Show("Email must have a valid email address format");
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            form.LoadAccounts_Click(sender, e);
            form.Show();
        }
    }
}
