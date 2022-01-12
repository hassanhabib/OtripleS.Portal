// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using OtripleS.Portal.Web.Infrastructure.Provision.Brokers.Clouds;
using OtripleS.Portal.Web.Infrastructure.Provision.Brokers.Loggings;

namespace OtripleS.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService()
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }
        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName}...");

            IResourceGroup resourceGroup =
                await this.cloudBroker.CreateResourceGroupAsync(
                    resourceGroupName);

            this.loggingBroker.LogActivity(message: $"{resourceGroupName} Provisioned.");

            return resourceGroup;
        }

        public async ValueTask<IAppServicePlan> ProvisionPlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string planName = $"{projectName}-PLAN-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {planName}...");

            IAppServicePlan plan =
                await this.cloudBroker.CreatePlanAsync(planName, resourceGroup);

            this.loggingBroker.LogActivity(message: $"{plan} Provisioned");

            return plan;
        }

        public async ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup,
            IAppServicePlan appServicePlan)
        {
            string webAppName = $"{projectName}-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {webAppName}");

            IWebApp webApp =
                await this.cloudBroker.CreateWebAppAsync(
                    webAppName,
                    appServicePlan,
                    resourceGroup);

            this.loggingBroker.LogActivity(message: $"{webAppName} Provisioned");

            return webApp;
        }

        public async ValueTask DeprovisionResouceGroupAsync(string projectName, string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();

            bool isResourceGroupExist =
                await this.cloudBroker.CheckResourceGroupExistAsync(
                    resourceGroupName);

            if (isResourceGroupExist)
            {
                this.loggingBroker.LogActivity(message: $"Deprovisioning {resourceGroupName}...");
                await this.cloudBroker.DeleteResourceGroupAsync(resourceGroupName);
                this.loggingBroker.LogActivity(message: $"{resourceGroupName} Deprovisioned");
            }
            else
            {
                this.loggingBroker.LogActivity(
                    message: $"Resource group {resourceGroupName} doesn't exist. No action taken.");
            }
        }
    }
}
