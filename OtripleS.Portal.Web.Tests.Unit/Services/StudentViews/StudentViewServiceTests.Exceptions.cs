// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.StudentViews
{
    public partial class StudentViewServiceTests
    {
        public static TheoryData StudentServiceValidationExceptions()
        {
            var innerException = new Exception();

            return new TheoryData<Exception>
            {
                new StudentValidationException(innerException),
                new StudentDependencyValidationException(innerException)
            };
        }

        [Theory]
        [MemberData(nameof(StudentServiceValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfStudentValidationErrorOccuredAndLogItAsync(
            Exception studentServiceValidationException)
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedDependencyValidationException =
                new StudentViewDependencyValidationException(studentServiceValidationException);

            this.studentServiceMock.Setup(service =>
                service.RegisterStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(studentServiceValidationException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewDependencyValidationException>(() =>
               addStudentViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.RegisterStudentAsync(It.IsAny<Student>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
