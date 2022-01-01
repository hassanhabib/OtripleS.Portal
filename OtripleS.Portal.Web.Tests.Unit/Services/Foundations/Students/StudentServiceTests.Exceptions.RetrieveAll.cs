// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalApiException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalDependencyExceptionOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    criticalDependencyException);

            var expectedStudentDependencyException =
                new StudentDependencyException(
                    failedStudentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Student>> retrievedStudentsTask =
                this.studentService.RetrieveAllStudentsAsync();

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
               retrievedStudentsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllStudentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyApiException))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception dependencyApiException)
        {
            // given
            var failedStudentDependencyException =
                new FailedStudentDependencyException(dependencyApiException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ThrowsAsync(dependencyApiException);
            // when
            ValueTask<List<Student>> retrievedStudentsTask =
                this.studentService.RetrieveAllStudentsAsync();

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                retrievedStudentsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllStudentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedStudentServiceExcption =
                new FailedStudentServiceException(serviceException);

            var expectedStudentServiceException =
                new StudentServiceException(failedStudentServiceExcption);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Student>> retrievedStudentTask =
                this.studentService.RetrieveAllStudentsAsync();

            // then
            await Assert.ThrowsAsync<StudentServiceException>(() =>
                retrievedStudentTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllStudentsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
