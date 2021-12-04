// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

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

        public CloudManagementService(
            ICloudBroker cloudBroker,
            ILoggingBroker loggingBroker)
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            throw new System.NotImplementedException();
        }
    }
}
