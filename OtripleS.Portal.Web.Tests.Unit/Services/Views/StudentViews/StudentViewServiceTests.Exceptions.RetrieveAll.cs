// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowStudentViewDependencyExceptionIfDependencyErrorOccursAndLogItAsync(
           Exception dependencyException)
        {
            // given
            var expectedStudentViewDependencyException =
                new StudentViewDependencyException(dependencyException);

            this.studentServiceMock.Setup(service =>
                service.RetrieveAllStudentsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<StudentView>> retrieveAllStudentViewsTask =
                this.studentViewService.RetrieveAllStudentViewsAsync();

            // then
            await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
                retrieveAllStudentViewsTask.AsTask());

            this.studentServiceMock.Verify(service =>
                service.RetrieveAllStudentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewDependencyException))),
                        Times.Once);

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowStudentViewServiceExceptionIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedStudentViewServiceException =
                new FailedStudentViewServiceException(serviceException);

            var expectedStudentViewServiceException =
                new StudentViewServiceException(failedStudentViewServiceException);

            this.studentServiceMock.Setup(service =>
                service.RetrieveAllStudentsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<StudentView>> retrieveAllStudentViewsTask =
                this.studentViewService.RetrieveAllStudentViewsAsync();

            // then
            await Assert.ThrowsAsync<StudentViewServiceException>(() =>
                retrieveAllStudentViewsTask.AsTask());

            this.studentServiceMock.Verify(service =>
                service.RetrieveAllStudentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewServiceException))),
                        Times.Once);

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
        }
    }
}
