// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.Loggings;
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Models.Students;

namespace OtripleS.Portal.Web.Services.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Student> RegisterStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudent(student);

            return await this.apiBroker.PostStudentAsync(student);
        });

        public ValueTask<List<Student>> RetrieveAllStudentsAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllStudentsAsync());
    }
}
