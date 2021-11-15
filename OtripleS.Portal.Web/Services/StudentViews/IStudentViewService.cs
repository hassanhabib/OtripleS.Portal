// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Models.StudentViews;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Services.StudentViews
{
    public interface IStudentViewService
    {
        ValueTask<StudentView> AddStudentViewAsync(StudentView studentView);
        void NavigateTo(string route);
    }
}
