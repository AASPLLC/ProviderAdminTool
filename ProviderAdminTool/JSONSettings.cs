namespace ProviderAdminTool
{
    public class JSONSettings
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
    }
}
