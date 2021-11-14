using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Courses;
using OtripleS.Portal.Web.Services.Courses;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Courses
{
    public partial class CourseServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICourseService courseService;

        public CourseServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.courseService = new CourseService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static List<Course> CreateRandomCourses() =>
            CreateCourseFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Course> CreateCourseFiller()
        {
            var filler = new Filler<Course>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
