// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.TeacherViews.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        public static TheoryData TeacherServiceDependencyExceptions()
        {
            var exception = new Exception();

            var teacherServiceDependencyException =
                new TeacherDependencyException(exception);

            var teacherServiceException =
                new TeacherServiceException(exception);

            return new TheoryData<Exception>
            {
                teacherServiceDependencyException,
                teacherServiceException
            };
        }

        [Theory]
        [MemberData(nameof(TeacherServiceDependencyExceptions))]
        public async Task ShouldThrowTeacherViewDependencyExceptionIfDependecyErrorOccursAndLogIt(
            Exception teacherDependencyException)
        {
            var expectedTeacherViewDependencyException =
                new TeacherViewDependencyException(teacherDependencyException);

            this.teacherServiceMock.Setup(teacherService =>
                teacherService.RetrieveAllTeachersAsync())
                    .Throws(teacherDependencyException);

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
    }
}
