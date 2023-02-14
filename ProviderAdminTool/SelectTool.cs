namespace ProviderAdminTool
{
    public partial class SelectTool : Form
    {
        public SelectTool()
        {
            InitializeComponent();
        }

        private void AccountsBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Accounts accounts = new(this);
            accounts.ShowDialog();
        }

        private void ProfilesBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Profiles profiles = new(this);
            profiles.ShowDialog();
        }
    }
}
