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

        private void DataverseBTN_Click(object sender, EventArgs e)
        {
            form.Init(0);
            Close();
        }

        private void CosmosBTN_Click(object sender, EventArgs e)
        {
            form.Init(1);
            Close();
        }
    }
}
