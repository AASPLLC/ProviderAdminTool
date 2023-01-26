namespace ProviderAdminTool
{
    public class JSONDataverseSettings
    {
        public string? ClientIDSecretName { get; set; }
        public string? ClientSecretSecretName { get; set; }
        public string? DBAccountsSecretName { get; set; }
        public string? DBSMSSecretName { get; set; }
        public string? DBWhatsAppSecretName { get; set; }
        public string? PhoneNumberColumnName { get; set; }
        public string? PhoneNumberIDAccountColumnName { get; set; }
        public string? PhoneNumberIDColumnName { get; set; }
        public string? EmailAccountColumnName { get; set; }
        public string? EmailNonAccountColumnName { get; set; }
        public string? ToColumnName { get; set; }
        public string? FromColumnName { get; set; }
        public string? StartingPrefix { get; set; }
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
    }
}
