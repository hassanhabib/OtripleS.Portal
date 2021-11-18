// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Courses;
using OtripleS.Portal.Web.Models.Courses.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Courses
{
    public partial class CourseServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalApiExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticialErrorOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedCourseDependencyException =
                new FailedCourseDependencyException(
                    criticalDependencyException);

            var expectedCourseDependencyException =
                new CourseDependencyException(
                    failedCourseDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCoursesAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Course>> retrievedAllCoursesTask =
                this.courseService.RetrieveAllCoursesAsync();

            // then
            await Assert.ThrowsAsync<CourseDependencyException>(() =>
               retrievedAllCoursesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCoursesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCourseDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyApiExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception dependencyApiException)
        {
            // given
            var failedCourseDependencyException =
                new FailedCourseDependencyException(dependencyApiException);

            var expectedCourseDependencyException =
                new CourseDependencyException(failedCourseDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCoursesAsync())
                    .ThrowsAsync(dependencyApiException);
            // when
            ValueTask<List<Course>> retrievedAllCoursesTask =
                this.courseService.RetrieveAllCoursesAsync();

            // then
            await Assert.ThrowsAsync<CourseDependencyException>(() =>
                retrievedAllCoursesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCoursesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCourseDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedCourseServiceExcption =
                new FailedCourseServiceException(serviceException);

            var expectedCourseServiceException =
                new CourseServiceException(failedCourseServiceExcption);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllCoursesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Course>> retrievedAllCoursesTask =
                this.courseService.RetrieveAllCoursesAsync();

            // then
            await Assert.ThrowsAsync<CourseServiceException>(() =>
                retrievedAllCoursesTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllCoursesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCourseServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
