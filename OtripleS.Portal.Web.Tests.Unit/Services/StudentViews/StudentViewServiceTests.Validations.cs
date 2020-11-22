// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentViewIsNullAndLogItAsync()
        {
            // given
            StudentView nullStudentView = null;
            var nullStudentViewException = new NullStudentViewException();

            var expectedStudentViewValidationException =
                new StudentViewValidationException(nullStudentViewException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(nullStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
               addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

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
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIdentityIsInvalidAndLogItAsync(
            string invalidIdentityNumber)
        {
            // given
            StudentView randomStudentView = CreateRandomStudentView();
            StudentView invalidStudentView = randomStudentView;
            invalidStudentView.IdentityNumber = invalidIdentityNumber;

            var invalidStudentViewException = new InvalidStudentViewException(
                parameterName: nameof(StudentView.IdentityNumber),
                parameterValue: invalidStudentView.IdentityNumber);

            var expectedStudentViewValidationException =
                new StudentViewValidationException(invalidStudentViewException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(invalidStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
               addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

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
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentFirstNameIsInvalidAndLogItAsync(
            string invalidFirstName)
        {
            // given
            StudentView randomStudentView = CreateRandomStudentView();
            StudentView invalidStudentView = randomStudentView;
            invalidStudentView.FirstName = invalidFirstName;

            var invalidStudentViewException = new InvalidStudentViewException(
                parameterName: nameof(StudentView.FirstName),
                parameterValue: invalidStudentView.FirstName);

            var expectedStudentViewValidationException =
                new StudentViewValidationException(invalidStudentViewException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(invalidStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
               addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

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
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentDateOfBirthIsInvalidAndLogItAsync()
        {
            // given
            StudentView randomStudentView = CreateRandomStudentView();
            StudentView invalidStudentView = randomStudentView;
            invalidStudentView.BirthDate = default;

            var invalidStudentViewException = new InvalidStudentViewException(
                parameterName: nameof(StudentView.BirthDate),
                parameterValue: invalidStudentView.BirthDate);

            var expectedStudentViewValidationException =
                new StudentViewValidationException(invalidStudentViewException);

            // when
            ValueTask<StudentView> addStudentViewTask =
                this.studentViewService.AddStudentViewAsync(invalidStudentView);

            // then
            await Assert.ThrowsAsync<StudentViewValidationException>(() =>
               addStudentViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentViewValidationException))),
                        Times.Once);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Never);

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
        }
    }
}
