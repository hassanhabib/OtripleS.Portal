// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Courses;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string CoursesRelativeUrl = "api/courses";

        public async ValueTask<List<Course>> GetAllCoursesAsync() =>
            await this.GetAsync<List<Course>>(CoursesRelativeUrl);
    }
}
