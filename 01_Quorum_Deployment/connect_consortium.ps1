$endpointAddress=""
$memberAccountAddress = ""
$memberAccountPassword = ""
$InformationPreference = ''
$SubscriptionId = ""
$RootContractAddress = ""


# Install and Import Module
Install-Module -Name Microsoft.AzureBlockchainService.ConsortiumManagement.PS -Scope CurrentUser
Import-Module Microsoft.AzureBlockchainService.ConsortiumManagement.PS

$Connection = New-Web3Connection -RemoteRPCEndpoint $endpointAddress
$MemberAccount = Import-Web3Account -ManagedAccountAddress $MemberAccountAddress -ManagedAccountPassword $MemberAccountPassword
$ContractConnection = Import-ConsortiumManagementContracts -RootContractAddress $RootContractAddress -Web3Client $Connection

#invite memebers to consortium

New-BlockchainMemberInvitation -SubscriptionId $SubscriptionId -Role ADMIN -Members $ContractConnection.Members -Web3Account $MemberAccount -Web3Client $Connection