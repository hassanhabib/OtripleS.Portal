// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
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
        [Fact]
        public async Task ShouldThrowTeacherViewDependencyExceptionIfDependecyErrorOccursAndLogIt()
        {
            var exception = new Exception();

            var failedTeacherViewDependencyException =
                new FailedTeacherViewDependencyException(exception);

            var expectedTeacherViewDependencyException =
                new TeacherViewDependencyException(failedTeacherViewDependencyException);

            this.teacherServiceMock.Setup(teacherService =>
                teacherService.RetrieveAllTeachersAsync())
                    .Throws(exception);

            ValueTask<List<TeacherView>> retrieveAllTeachersTask = 
                this.teacherViewService.RetrieveAllTeachers();

            await Assert.ThrowsAsync<TeacherViewDependencyException>(() => 
                retrieveAllTeachersTask.AsTask());

            this.teacherServiceMock.Verify(teacherService => 
                teacherService.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.teacherServiceMock.VerifyNoOtherCalls();
        } 
    }
}
