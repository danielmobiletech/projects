resource storageaccountn 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: 'mycastorageaccount90987'
  location: 'westus'
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
 }
