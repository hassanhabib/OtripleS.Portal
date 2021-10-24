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
        public static TheoryData CriticalApiExceptions()
        {
            string exceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseUnAuthorizedException =
                new HttpResponseUnauthorizedException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpResponseUrlNotFoundException,
                httpResponseUnAuthorizedException
            };
        }

        [Theory]
        [MemberData(nameof(CriticalApiExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalApiExceptionOccursAndLogItAsync(
            Exception criticalApiException)
        {
            var failedRequestToTeacherDependencyException =
                new FailedRequestToTeacherDependencyException(criticalApiException);

            var expectedTeacherDependencyException =
                new TeacherDependencyException(
                    failedRequestToTeacherDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ThrowsAsync(criticalApiException);

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
                    .Throws(httpResponseException);

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

            var expectedTeacherServiceException = 
                new TeacherServiceException(serviceException);

            this.apiBrokerMock.Setup(broker => 
                broker.GetAllTeachersAsync())
                    .Throws(serviceException);

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
