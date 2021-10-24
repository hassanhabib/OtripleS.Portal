// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.API;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Services.Teachers
{
    public partial class TeacherService : ITeacherService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public TeacherService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<Teacher>> RetrieveAllTeachersAsync() =>
        TryCatch(async () => 
        {
            List<Teacher> teachers =
                await this.apiBroker.GetAllTeachersAsync();

            ValidateGetAllTeachersApiResponse(teachers);

            return teachers;
        });
    }
}
