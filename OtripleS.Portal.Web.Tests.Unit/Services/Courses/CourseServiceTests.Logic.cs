// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OtripleS.Portal.Web.Models.Courses;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Courses
{
    public partial class CourseServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllCoursesAsync()
        {
            // given
            List<Course> randomCourses = CreateRandomCourses();
            List<Course> rerievedCourses = randomCourses;
            List<Course> expetedCourses = rerievedCourses.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCoursesAsync())
                    .ReturnsAsync(rerievedCourses);

            // when
            List<Course> actualCourses =
                await this.courseService.RetrieveAllCoursesAsync();

            // then
            actualCourses.Should().BeEquivalentTo(expetedCourses);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCoursesAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
