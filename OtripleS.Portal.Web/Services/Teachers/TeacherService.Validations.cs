using System.Collections.Generic;
using System.Linq;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Services.Teachers
{
    public partial class TeacherService
    {
        private void ValidateGetAllTeachersApiResponse(IReadOnlyList<Teacher> teachers)
        {
            if (!teachers.Any())
            {
                this.loggingBroker.LogWarning("No teachers retrieved from the api.");
            }
        }
    }
}
