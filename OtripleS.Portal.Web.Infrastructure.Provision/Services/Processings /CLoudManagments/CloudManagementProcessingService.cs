// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using OtripleS.Portal.Web.Infrastructure.Provision.Brokers.Configurations;
using OtripleS.Portal.Web.Infrastructure.Provision.Models.Configurations;
using OtripleS.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements;

namespace OtripleS.Portal.Web.Infrastructure.Provision.Services.Processings.CLoudManagments
{
	public class CloudManagementProcessingService : ICloudManagementProcessingService
	{
		private readonly ICloudManagementService cloudManagementService;
		private readonly IConfigurationBroker configurationBroker;

		public CloudManagementProcessingService()
		{
			this.cloudManagementService = new CloudManagementService();
			this.configurationBroker = new ConfigurationBroker();
		}

		public async ValueTask ProcessAsync()
		{
			CloudManagementConfiguration cloudManagementConfiguration =
				this.configurationBroker.GetConfiguration();

			await ProvisionAsync(
				projectName: cloudManagementConfiguration.ProjectName,
				cloudAction: cloudManagementConfiguration.Up);

			await DeprovisionAsync(
				projectName: cloudManagementConfiguration.ProjectName,
				cloudAction: cloudManagementConfiguration.Down);
		}

		private async ValueTask ProvisionAsync(
			string projectName,
			CloudAction cloudAction)
		{
			List<string> environments = RetrieveEnvironments(cloudAction);

			foreach (string environmentName in environments)
			{
				IResourceGroup resourceGroup = await this.cloudManagementService
					.ProvisionResourceGroupAsync(
						projectName,
						environmentName);

				IAppServicePlan appServicePlan = await this.cloudManagementService
					.ProvisionPlanAsync(
						projectName,
						environmentName,
						resourceGroup);

				IWebApp webApp = await this.cloudManagementService
					.ProvisionWebAppAsync(
						projectName,
						environmentName,
						resourceGroup,
						appServicePlan);
			}
		}

		private async ValueTask DeprovisionAsync(
			string projectName,
			CloudAction cloudAction)
		{
			List<string> environments = RetrieveEnvironments(cloudAction);

			foreach (string environmentName in environments)
			{
				await this.cloudManagementService.DeprovisionResouceGroupAsync(
					projectName,
					environmentName);
			}
		}

		private static List<string> RetrieveEnvironments(CloudAction cloudAction) =>
			cloudAction?.Environmnents ?? new List<string>();
	}
}