// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using RESTFulSense.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalDependencyExceptionOccursAndLogItAsync(
            Exception criticalDependencyException)
        {
            var failedTeacherDependencyException =
                new FailedTeacherDependencyException(criticalDependencyException);

            var expectedTeacherDependencyException =
                new TeacherDependencyException(
                    failedTeacherDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ThrowsAsync(criticalDependencyException);

            ValueTask<List<Teacher>> retrieveAllTeachersTask =
                this.teacherService.RetrieveAllTeachersAsync();

            await Assert.ThrowsAsync<TeacherDependencyException>(() =>
               retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTeacherDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowTeacherDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync()
        {
            var randomExceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: randomExceptionMessage);

            var failedTeacherDependencyException =
                new FailedTeacherDependencyException(httpResponseException);

            var expectedDependencyException =
                new TeacherDependencyException(
                    failedTeacherDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ThrowsAsync(httpResponseException);

            ValueTask<List<Teacher>> retrieveAllTeachersTask =
                teacherService.RetrieveAllTeachersAsync();

            await Assert.ThrowsAsync<TeacherDependencyException>(() =>
                retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameValidationExceptionAs(
                        expectedDependencyException))),
                            Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            var serviceException = new Exception();

            var failedTeacherServiceException =
                new FailedTeacherServiceException(serviceException);

            var expectedTeacherServiceException =
                new TeacherServiceException(failedTeacherServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Teacher>> retrieveAllTeachersTask =
                this.teacherService.RetrieveAllTeachersAsync();

            // then
            await Assert.ThrowsAsync<TeacherServiceException>(() =>
               retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTeacherServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
