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
            this.navigationBrokerMock.VerifyNoOtherCalls();
        }

        public static TheoryData StudentServiceDependencyExceptions()
        {
            var innerException = new Exception();

            return new TheoryData<Exception>
            {
                new StudentDependencyException(innerException),
                new StudentServiceException(innerException)
            };
        }

        [Theory]
        [MemberData(nameof(StudentServiceDependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfStudentDependencyErrorOccuredAndLogItAsync(
            Exception studentServiceDependencyException)
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();

            var expectedStudentDependencyException =
                new StudentViewDependencyException(studentServiceDependencyException);

            this.studentServiceMock.Setup(service =>
                service.RegisterStudentAsync(It.IsAny<Student>()))
                    .ThrowsAsync(studentServiceDependencyException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewDependencyException>(() =>
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
                    expectedStudentDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccuredAndLogItAsync()
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();
            var serviceException = new Exception();

            var expectedStudentServiceException =
                new StudentViewServiceException(serviceException);

            this.userServiceMock.Setup(service =>
                service.GetCurrentlyLoggedInUser())
                    .Throws(serviceException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(someStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewServiceException>(() =>
               addStudentViewTask.AsTask());

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentServiceException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.studentServiceMock.Verify(service =>
                service.RegisterStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnNavigateIfServiceErrorOccursAndLogIt()
        {
            // given
            string someRoute = GetRandomRoute();
            var serviceException = new Exception();

            var expectedStudentViewServiceException =
                new StudentViewServiceException(serviceException);

            this.navigationBrokerMock.Setup(broker =>
                broker.NavigateTo(It.IsAny<string>()))
                    .Throws(serviceException);

            // when
            Action navigateToAction = () =>
                this.studentViewService.NavigateTo(someRoute);

            // then
            Assert.Throws<StudentViewServiceException>(navigateToAction);

            this.navigationBrokerMock.Verify(broker =>
                broker.NavigateTo(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewServiceException))),
                        Times.Once);

            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
