using System.Text.RegularExpressions;
using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class AddNewUser : Form
    {
        Form1 form = new();
        string keyvault = "";
        JSONSettings Settings = new();
        DataverseHandler dh = new();

        public AddNewUser()
        {
            InitializeComponent();
        }

        public void SetupForm(Form1 form, string keyvault, JSONSettings Settings, DataverseHandler dh)
        {
            this.form = form;
            this.keyvault = keyvault;
            this.Settings = Settings;
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

                    await dh.CreateAccountDB(Settings.ClientIDSecretName, Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.DBAccountsSecretName, keyvault, emailTB.Text, smsNumberTB.Text, whatsappIDTB.Text);
                    //await DataverseHandler.CreateAccountDBSecret(Settings.ClientIDSecretName, Settings.ClientSecretSecretName, Settings.PhoneNumberColumnName, Settings.EmailAccountColumnName, Settings.PhoneNumberIDAccountColumnName, Settings.DBAccountsSecretName, keyvault, environment, emailTB.Text, smsNumberTB.Text, whatsappIDTB.Text);
                    form.Button1_Click(sender, e);
                    form.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("SMS Phone Number & WhatsApp Phone Number ID can only contain numbers.");
            }
            else
                MessageBox.Show("Email must have a valid email address format");
        }
    }
}
