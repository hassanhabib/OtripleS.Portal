// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
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
	}

}
