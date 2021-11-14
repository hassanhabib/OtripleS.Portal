using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Courses;

namespace OtripleS.Portal.Web.Services.Courses
{
    public interface ICourseService
    {
        ValueTask<List<Course>> RetrieveAllCoursesAsync();
    }
}