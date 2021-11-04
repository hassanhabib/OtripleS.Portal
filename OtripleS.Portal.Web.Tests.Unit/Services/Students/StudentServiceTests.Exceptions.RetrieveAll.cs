// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using RESTFulSense.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Students
{
    public partial class StudentServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalApiExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalDependencyExceptionOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            // given
            var failedStudentDependencyException =
                new FailedStudentDependencyException(
                    criticalDependencyException);

            var expectedStudentDepndencyException =
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
                    expectedStudentDepndencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowStudentDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync()
        {
            // given
            var randomExceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: randomExceptionMessage);

            var failedStudentDependencyException =
                new FailedStudentDependencyException(httpResponseException);

            var expectedStudentDependencyException =
                new StudentDependencyException(failedStudentDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ThrowsAsync(httpResponseException);
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
    }
}
