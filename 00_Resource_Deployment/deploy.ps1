$resourceGroupName=''
$location=''
$storageAccountName='' #needs to be lower case
$cosmosAccountName='transactiondata'
$databaseName='ContractTransaction'
$subscriptionID=''

az login

az account set --subscription $subscriptionID

az group create `
    --location $location `
    --name $resourceGroupName `
    --subscription $subscriptionID

# Create a MongoDB API Cosmos DB account with consistent prefix (Local) consistency and multi-master enabled
az cosmosdb create `
    --resource-group $resourceGroupName `
    --name $cosmosAccountName `
    --kind MongoDB `
    --locations "southcentralus=0" `
    --default-consistency-level "ConsistentPrefix" `
    --enable-multiple-write-locations false `
    --subscription $subscriptionID `


# Create a database 
az cosmosdb database create `
    --resource-group-name $resourceGroupName `
    --db-name $databaseName `
    --name $cosmosAccountName

az cosmosdb collection create `
    --resource-group-name $resourceGroupName `
    --db-name $databaseName `
    --collection-name "contractransactions" `
    --name $cosmosAccountName

az cosmosdb collection create `
    --resource-group-name $resourceGroupName `
    --db-name $databaseName `
    --collection-name "Vendors" `
    --name $cosmosAccountName