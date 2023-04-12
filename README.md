$projectname="adtstore"
$appreg="adtstore"
az account set --subscription SPD.MTV.2022
az ad sp create-for-rbac --name ${appreg} --role Contributor --scopes /subscriptions/851a3e64-bc9a-4d4c-b20e-fde1b7055938 --skip-assignment > AppCredentials.txt
$objectid=$(az ad sp list --display-name ${appreg} --query [0].id --output tsv)
$userid=$(az ad signed-in-user show --query id -o tsv)
az group create --name ${projectname}-rg --location eastus
az deployment group create -f azuredeploy.bicep -g ${projectname}-rg --parameters projectName=${projectname} userId=${userid} appRegObjectId=${objectid} > ARM_deployment_out.txt
az deployment group show -n azuredeploy -g ${projectname}-rg --query properties.outputs.importantInfo.value > Azure_config_settings.txt
az iot hub connection-string show --resource-group ${projectname}-rg >> Azure_config_settings.txt
