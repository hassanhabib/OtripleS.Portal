// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Infrastructure.Provision.Models.Configirations;

namespace OtripleS.Portal.Web.Infrastructure.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        CloudManagementConfiguration GetConfigurations();
    }
}
