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
            string exceptionMessage = GetRandomText();
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
            Exception httpResponseCriticalException)
        {
            var expectedTeacherDependencyException =
                new TeacherDependencyException(
                    httpResponseCriticalException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ThrowsAsync(httpResponseCriticalException);

            ValueTask<IReadOnlyList<Teacher>> retrieveAllTeachersTask =
                this.teacherService.RetrieveAllTeachersAsync();

            await Assert.ThrowsAsync<TeacherDependencyException>(() =>
               retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllTeachersAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(
                    SameExceptionAs(expectedTeacherDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        public static TheoryData DependencyApiExceptions()
        {
            string exceptionMessage = GetRandomText();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseInternalServerErrorException =
                new HttpResponseInternalServerErrorException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseException,
                httpResponseInternalServerErrorException
            };
        }

        [Theory]
        [MemberData(nameof(DependencyApiExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyApiErrorOccursAndLogItAsync(
            Exception dependencyApiException)
        {
            var randomExceptionMessage = GetRandomText();
            var responseMessage = new HttpResponseMessage();

            var expectedDependencyException = 
                new TeacherDependencyException(dependencyApiException);

            this.apiBrokerMock.Setup(apiBroker => 
                apiBroker.GetAllTeachersAsync())
                .Throws(dependencyApiException);

            var retrieveAllTeachersTask =
                teacherService.RetrieveAllTeachersAsync();

            await Assert.ThrowsAsync<TeacherDependencyException>(() => 
                retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(loggingBroker =>
                loggingBroker.LogError(It.Is(SameValidationExceptionAs(expectedDependencyException))));

            apiBrokerMock.VerifyNoOtherCalls();
            loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionRetrieveAllIfErrorOccursAndLogItAsync()
        {
            var serviceException = new Exception();

            var expectedTeacherServiceException = 
                new TeacherServiceException(serviceException);

            this.apiBrokerMock.Setup(apiBroker => 
                apiBroker.GetAllTeachersAsync())
                .Throws(serviceException);

            // when
            var retrieveAllTeachersTask =
                this.teacherService.RetrieveAllTeachersAsync();

            // then
            await Assert.ThrowsAsync<TeacherServiceException>(() =>
               retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(),
                Times.Once);

            this.loggingBrokerMock.Verify(loggingBroker =>
                loggingBroker.LogError(It.Is(
                    SameExceptionAs(expectedTeacherServiceException))),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
