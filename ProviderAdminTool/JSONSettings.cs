namespace ProviderAdminTool
{
    public class JSONDataverseSettings
    {
        public string? ClientIDSecretName { get; set; }
        public string? ClientSecretSecretName { get; set; }
        public string? AccountsDBEndingPrefix { get; set; }
        public string? SMSDBEndingPrefix { get; set; }
        public string? WhatsAppDBEndingPrefix { get; set; }
        public string? PhoneNumberAccountColumnName { get; set; }
        public string? PhoneNumberIDAccountColumnName { get; set; }
        public string? EmailAccountColumnName { get; set; }
        public string? EmailNonAccountColumnName { get; set; }
        public string? ToColumnName { get; set; }
        public string? FromColumnName { get; set; }
        public string? PhoneNumbersDBEndingPrefix { get; set; }
        public string? PhoneNumberProfileColumnName { get; set; }
        public string? PicturePathProfileColumnName { get; set; }
        public string? DisplayNameProfileColumnName { get; set; }
        public string? StartingPrefix { get; set; }
        public string? Environment { get; set; }
    }

    public class JSONCosmosSettings
    {
        public string? smsIDName { get; set; }
        public string? whatsappIDName { get; set; }
        public string? accountsIDName { get; set; }
        public string? countersIDName { get; set; }
        public string? smsContainerName { get; set; }
        public string? whatsappContainerName { get; set; }
        public string? accountsContainerName { get; set; }
        public string? countersContainerName { get; set; }
        public string? RestSiteSecretName { get; set; }
    }
}
