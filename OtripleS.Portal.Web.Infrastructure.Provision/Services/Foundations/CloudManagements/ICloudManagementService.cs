// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace OtripleS.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public interface ICloudManagementService
	{
		ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
			string projectName,
			string environment);

		ValueTask<IAppServicePlan> ProvisionPlanAsync(
			string projectName,
			string environment,
			IResourceGroup resourceGroup);

		ValueTask<IWebApp> ProvisionWebAppAsync(
			string projectName,
			string environment,
			IResourceGroup resourceGroup,
			IAppServicePlan appServicePlan);
	}
}
