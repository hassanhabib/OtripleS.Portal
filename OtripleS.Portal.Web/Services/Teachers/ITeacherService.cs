// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Models.Teachers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Services.Teachers
{
    public interface ITeacherService
    {
        ValueTask<List<Teacher>> RetrieveAllTeachersAsync();
    }
}
