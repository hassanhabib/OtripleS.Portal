// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Infrastructure.Provision.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public void LogActivity(string message) =>
            Console.WriteLine(message);
    }
}
