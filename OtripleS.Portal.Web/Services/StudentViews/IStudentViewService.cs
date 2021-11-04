// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.StudentViews;

namespace OtripleS.Portal.Web.Services.StudentViews
{
    public interface IStudentViewService
    {
        ValueTask<StudentView> AddStudentViewAsync(StudentView studentView);
        void NavigateTo(string route);
        ValueTask<List<StudentView>> RetrieveAllStudentsViewAsync();
    }
}
