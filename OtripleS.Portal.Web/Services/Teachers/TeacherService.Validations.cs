using OtripleS.Portal.Web.Models.Teachers;
using System.Collections.Generic;
using System.Linq;

namespace OtripleS.Portal.Web.Services.Teachers
{
    public partial class TeacherService
    {
        private void ValidateGetAllTeachersApiResponse(IList<Teacher> teachers)
        {
            if (!teachers.Any())
            {
                this.loggingBroker.LogWarning("No teachers retrieved from the api.");
            }
        }
    }
}
