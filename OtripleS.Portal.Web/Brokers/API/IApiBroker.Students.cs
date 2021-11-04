// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Students;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        ValueTask<Student> PostStudentAsync(Student student);
        ValueTask<List<Student>> GetAllStudentsAsync();
    }
}
