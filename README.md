$projectname="adtwind"
$appreg="adtwind"
az account set --subscription SPD.MTV.2022
az ad sp create-for-rbac --name ${appreg} --role Contributor --scopes /subscriptions/851a3e64-bc9a-4d4c-b20e-fde1b7055938 --skip-assignment > AppCredentials.txt
$objectid=$(az ad sp list --display-name ${appreg} --query [0].id --output tsv)
$userid=$(az ad signed-in-user show --query id -o tsv)
az group create --name ${projectname}-rg --location eastus
az deployment group create -f azuredeploy.bicep -g ${projectname}-rg --parameters projectName=${projectname} userId=${userid} appRegObjectId=${objectid} > ARM_deployment_out.txt
az deployment group show -n azuredeploy -g ${projectname}-rg --query properties.outputs.importantInfo.value > Azure_config_settings.txt
az iot hub connection-string show --resource-group ${projectname}-rg >> Azure_config_settings.txt





# Blade Infra 
## Required Installs
Azure CLI version 2.20.0 or higher: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli

**Note: The ARM template is in the Bicep format, and if an earlier version of Azure CLI is used, you might get an “invalid JSON” error.**

## CLI
### 1. Clone this Repo

### 2. Set some vars on the CLI for use
* Projectname: the overall unique name for your resources
* appreg: the name of the application registration for your hololens app
* username: your UPN in Azure


```
$projectname="adtauto"
$appreg="adtauto"
```

### 3. Create App Registration
`
az ad sp create-for-rbac --name ${appreg} --skip-assignment
`


Save Output for later

### 4. Get ObjectID of App Registation and User and assign to envvar
```
$objectid=$(az ad sp list --display-name ${appreg} --query [0].id --output tsv)
$userid=$(az ad signed-in-user show --query id -o tsv)
```
you can echo this `echo $objectid` and `echo $userid` to check it has a value


### 5. Create Resource Group
`
az group create --name ${projectname}-rg --location eastus
`

### 6. Deploy ARM template to Resource Group
`
az deployment group create -f azuredeploy.bicep -g ${projectname}-rg --parameters projectName=${projectname} userId=${userid} appRegObjectId=${objectid} > ARM_deployment_out.txt
`
- Denote the output values here for the Azure Digital Twins URL and FunctionApp SignalR URL. These will be used later

### 7. Get IotHub Connection String
az extension add --name azure-iot

az deployment group show -n azuredeploy -g ${projectname}-rg --query properties.outputs.importantInfo.value > Azure_config_settings.txt

az iot hub connection-string show --resource-group ${projectname}-rg >> Azure_config_settings.txt

get-content Azure_config_settings.txt

- Save this for later use

### 7. Add values to the Device Sim and Test
