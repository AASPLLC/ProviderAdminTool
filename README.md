# ProviderAdminTool
Provder Admin Tool - This allows admins to quickly and easily create, edit, and delete user accounts in Dataverse only. Cosmos will need to be manually managed in portal.azure.com with secured access at this time.

The following will need to be provided manually for security reasons:

The Settings.json file is specific to Dataverse and is required for the admin application to function correctly.

These values must be specific, but for reference, this is the format:
```
{
  "ClientIDSecretName": "",
  "ClientSecretSecretName": "",
  "DBAccountsSecretName": "",
  "DBSMSSecretName": "",
  "DBWhatsAppSecretName": "",
  "PhoneNumberColumnName": "",
  "PhoneNumberIDAccountColumnName": "",
  "PhoneNumberIDColumnName": "",
  "EmailAccountColumnName": "",
  "EmailNonAccountColumnName": "",
  "ToColumnName": "",
  "FromColumnName": ""
}
```

This application is dependent on the following library: [AASP Global Library](https://github.com/AASPWayne/AASPGlobalLibrary)
