using AASPGlobalLibrary;

namespace ProviderAdminTool
{
    public partial class ChooseDBType : Form
    {
        public Form1 form = new();

        public ChooseDBType(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Globals.OpenLink("https://digitalpocketdevelopment.sharepoint.com/:w:/r/sites/DigitalPocketDeveloment-Test2/_layouts/15/Doc.aspx?sourcedoc=%7BEBE2A2F7-FB72-45B6-857B-844A27B69083%7D&file=DatabaseTypes.docx&action=default&mobileredirect=true");
        }

        private async void DataverseBTN_Click(object sender, EventArgs e)
        {
            cosmosBTN.Enabled = false;
            dataverseBTN.Enabled = false;
            dynamic? globalsjson = await Globals.LoadJSONDynamic(Environment.CurrentDirectory + "/Globals.json");
#pragma warning disable CS8601
            form.vaultname = globalsjson?.VaultName;

            form.DataverseSettings = await Globals.LoadJSON<JSONDataverseSettings>(Environment.CurrentDirectory + "/DataverseSettings.json");
#pragma warning restore CS8601
            form.Init(0, await VaultHandler.GetSecretInteractive(form.vaultname, form.DataverseSettings.Environment));
            Close();
        }

        private async void CosmosBTN_Click(object sender, EventArgs e)
        {
            cosmosBTN.Enabled = false;
            dataverseBTN.Enabled = false;
            dynamic? globalsjson = await Globals.LoadJSONDynamic(Environment.CurrentDirectory + "/Globals.json");
#pragma warning disable CS8601
            form.vaultname = globalsjson?.VaultName;
            form.CosmosSettings = await Globals.LoadJSON<JSONCosmosSettings>(Environment.CurrentDirectory + "/CosmosSettings.json");
#pragma warning restore CS8601
            form.Init(1);
            Close();
        }
    }
}
