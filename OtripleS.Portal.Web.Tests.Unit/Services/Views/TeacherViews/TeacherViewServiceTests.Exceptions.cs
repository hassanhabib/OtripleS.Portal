// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.TeacherViews.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Views.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowTeacherViewDependencyExceptionIfDependecyErrorOccursAndLogItAsync(
            Exception dependencyException)
        {
            var expectedTeacherViewDependencyException =
                new TeacherViewDependencyException(dependencyException);

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ThrowsAsync(dependencyException);

            ValueTask<List<TeacherView>> retrieveAllTeacherViewsTask =
                this.teacherViewService.RetrieveAllTeacherViewsAsync();

            await Assert.ThrowsAsync<TeacherViewDependencyException>(() =>
                retrieveAllTeacherViewsTask.AsTask());

            this.teacherServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherViewDependencyException))),
                        Times.Once);

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowTeacherViewServiceExceptionWhenServiceErrorOccursAndLogItAsync()
        {
            var serviceException = new Exception();

            var failedTeacherViewServiceException =
                new FailedTeacherViewServiceException(serviceException);

            var expectedTeacherViewServiceException =
                new TeacherViewServiceException(failedTeacherViewServiceException);

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ThrowsAsync(serviceException);

            ValueTask<List<TeacherView>> retrieveAllTeacherViewsTask =
                this.teacherViewService.RetrieveAllTeacherViewsAsync();

            await Assert.ThrowsAsync<TeacherViewServiceException>(() =>
                retrieveAllTeacherViewsTask.AsTask());

            this.teacherServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherViewServiceException))),
                        Times.Once);

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
