// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using System.Collections.Generic;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Services
{
    public interface ITeacherService
    {
        ValueTask<IEnumerable<Teacher>> RetrieveAllTeachersAsync();
    }
}
