// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRegisterIfStudentIsNullAndLogItAsync()
        {
            // given
            Student invalidStudent = null;
            var nullStudentException = new NullStudentException();

            var expectedStudentValidationException =
                new StudentValidationException(nullStudentException);

            // when
            ValueTask<Student> submitStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                submitStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            var invalidStudent = new Student
            {
                IdentityNumber = invalidString,
                FirstName = invalidString,
                UserId = invalidString
            };

            var invalidStudentException = new InvalidStudentException();

            invalidStudentException.AddData(
                key: nameof(Student.Id),
                values: "Id is required");

            invalidStudentException.AddData(
                key: nameof(Student.UserId),
                values: "Id is required");

            invalidStudentException.AddData(
                key: nameof(Student.IdentityNumber),
                values: "Id is required");

            invalidStudentException.AddData(
                key: nameof(Student.FirstName),
                values: "Text is required");

            invalidStudentException.AddData(
                key: nameof(Student.BirthDate),
                values: "Date is required");

            invalidStudentException.AddData(
                key: nameof(Student.CreatedDate),
                values: "Date is required");

            invalidStudentException.AddData(
                key: nameof(Student.UpdatedDate),
                values: "Date is required");
            
            invalidStudentException.AddData(
                key: nameof(Student.CreatedBy),
                values: "Id is required");
            
            invalidStudentException.AddData(
                key: nameof(Student.UpdatedBy),
                values: "Id is required");

            var expectedStudentValidationException =
                new StudentValidationException(invalidStudentException);

            // when
            ValueTask<Student> addStudentTask =
                this.studentService.AddStudentAsync(invalidStudent);

            // then
            await Assert.ThrowsAsync<StudentValidationException>(() =>
                addStudentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(It.IsAny<Student>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
