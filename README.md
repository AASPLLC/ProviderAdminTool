# ProviderAdminTool
Provder Admin Tool - This allows admins to quickly and easily create, edit, and delete user accounts in Cosmos & Dataverse.

This files needs to be located in the same location as the application.

The following will need to be provided manually and must be specific for security reasons.

The DataverseSettings.json file is specific to Dataverse and is required for the admin application to function correctly:
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
  "StartingPrefix": "smsapp_",
  "Environment": "DynamicsEnvironmentName"
}
```
The CosmosSettings.json file is specific to Cosmos and is required for the admin application to function correctly:
```
{
  "smsIDName": "",
  "whatsappIDName": "",
  "accountsIDName": "",
  "countersIDName": "",
  "smsContainerName": "",
  "whatsappContainerName": "",
  "accountsContainerName": "",
  "countersContainerName": "",
  "RestSiteSecretName": ""
}
```
The Globals.json file is settings to reduce reptitive input is required for the admin application to function correctly:
```
{
  "VaultName": ""
}
```

This application is dependent on the following library: [AASP Global Library](https://github.com/wrharper/AASPGlobalLibrary)
