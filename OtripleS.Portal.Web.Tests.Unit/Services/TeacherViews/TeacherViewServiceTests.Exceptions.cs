// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.TeacherViews.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        public static TheoryData TeacherServiceExceptions()
        {
            var innerException = new Exception();

            var teacherServiceDependencyException =
                new TeacherDependencyException(innerException);

            var teacherServiceException =
                new TeacherServiceException(innerException);

            return new TheoryData<Exception>
            {
                teacherServiceDependencyException,
                teacherServiceException
            };
        }

        [Theory]
        [MemberData(nameof(TeacherServiceExceptions))]
        public async Task ShouldThrowTeacherViewDependencyExceptionIfDependecyErrorOccursAndLogIt(
            Exception teacherServiceException)
        {
            var expectedTeacherViewDependencyException =
                new TeacherViewDependencyException(teacherServiceException);

            this.teacherServiceMock.Setup(teacherService =>
                teacherService.RetrieveAllTeachersAsync())
                    .Throws(teacherServiceException);

            ValueTask<List<TeacherView>> retrieveAllTeachersTask = 
                this.teacherViewService.RetrieveAllTeachers();

            await Assert.ThrowsAsync<TeacherViewDependencyException>(() => 
                retrieveAllTeachersTask.AsTask());

            this.teacherServiceMock.Verify(teacherService => 
                teacherService.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(loggingBroker =>
                loggingBroker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherViewDependencyException))),
                        Times.Once);

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowTeacherViewServiceExceptionWhenServiceErrorOccursAndLogIt()
        {
            var innerException = new Exception();

            var failedTeacherViewServiceException =
                new FailedTeacherViewServiceException(innerException);

            var expectedTeacherViewServiceException =
                new TeacherViewServiceException(failedTeacherViewServiceException);

            this.teacherServiceMock.Setup(teacherService =>
                teacherService.RetrieveAllTeachersAsync())
                    .Throws(innerException);

            ValueTask<List<TeacherView>> retrieveAllTeachersTask =
                this.teacherViewService.RetrieveAllTeachers();

            await Assert.ThrowsAsync<TeacherViewServiceException>(() =>
                retrieveAllTeachersTask.AsTask());

            this.teacherServiceMock.Verify(teacherService =>
                teacherService.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(loggingBroker =>
                loggingBroker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherViewServiceException))),
                        Times.Once);

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
