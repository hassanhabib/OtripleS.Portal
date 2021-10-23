// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Models.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string CoursesRelativeUrl = "api/courses";

        public async ValueTask<IEnumerable<Course>> GetAllCoursesAsync() =>
            await this.GetAsync<IEnumerable<Course>>(CoursesRelativeUrl);
    }
}
