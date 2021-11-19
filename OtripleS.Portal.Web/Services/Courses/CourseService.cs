// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Brokers.Loggings;
using OtripleS.Portal.Web.Models.Courses;

namespace OtripleS.Portal.Web.Services.Courses
{
    public partial class CourseService : ICourseService
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

        public ValueTask<List<Course>> RetrieveAllCoursesAsync() =>
        TryCatch(async () => await this.apiBroker.GetAllCoursesAsync());
    }
}
