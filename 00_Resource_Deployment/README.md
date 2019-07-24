# Resource Deployment

This folder contains a PowerShell script that can be used to provision the Cosmos DB account, database, and collections required to build your Blockchain solution.  You may skip this folder if you prefer to provision your Azure resources via the Azure Portal.  The PowerShell script will provision the following resources to your Azure subscription:

 
| Resource              | Usage                                                                                     |
|-----------------------|-------------------------------------------------------------------------------------------|
| [Azure Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/)  | The patient information stored as a document          |

## Prerequisites
1. Access to an Azure Subscription
2. Azure CLI Installed

## Deploy via Azure Portal
As an alternative to running the PowerShell script, you can deploy the resources manually via the Azure Portal.

## Steps for Resource Deployment via PowerShell

To run the [PowerShell script](./deploy.ps1):

1. Modify the parameters at the top of **deploy.ps1** to configure the names of your resources and other settings.   
2. Run the [PowerShell script](./deploy.ps1). If you have PowerShell opened to this folder run the command:
`./deploy.ps1`
3. You will then be prompted to login and provide additional information
4. Following deployment, open the Cosmos DB account
5. Navigate to Data Explorer
6. Add your vendors using the following format

For each Vendor, insert a document with this format.

```json
{
    "_id" : ObjectId("5d029f0a7dfa952538029fd3"),
    "type" : "HOTEL",
    "name" : "EXAMPLE_HOTEL"
}
```
