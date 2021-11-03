// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Students;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string StudentsRelativeUrl = "api/students";

        public async ValueTask<Student> PostStudentAsync(Student student) =>
            await this.PostAsync(StudentsRelativeUrl, student);

        public async ValueTask<List<Student>> GetAllStudentAsync() =>
            await this.GetAsync<List<Student>>(StudentsRelativeUrl);
    }
}
