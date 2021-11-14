using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Courses;

namespace OtripleS.Portal.Web.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public CourseService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<Course>> RetrieveAllCoursesAsync()
        {
            return await this.apiBroker.GetAllCoursesAsync();
        }
    }
}
