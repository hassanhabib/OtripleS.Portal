// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Models.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Services.Students
{
    public interface IStudentService
    {
        ValueTask<Student> RegisterStudentAsync(Student student);
        ValueTask<List<Student>> RetrieveAllStudentsAsync();
    }
}
