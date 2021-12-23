$resourcegroup="424-16bdb2cc-create-web-app-from-docker-container";
$container="newrgregs"

git clone --branch js-docker https://github.com/linuxacademy/content-AZ-104-Microsoft-Azure-Administrator.git ./js-docker

Connect-AzAccount
New-AzContainerRegistry -Name $container -ResourceGroupName $resourcegroup -Sku Basic -EnableAdminUser 
cd js-docker/

