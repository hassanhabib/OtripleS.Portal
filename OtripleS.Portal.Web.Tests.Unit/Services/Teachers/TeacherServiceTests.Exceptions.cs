// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using RESTFulSense.Exceptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfInternalServerErrorOccursAndLogItAsync()
        {
            var randomExceptionMessage = GetRandomText();
            var responseMessage = new HttpResponseMessage();

            var internalServerErrorException = 
                new HttpResponseInternalServerErrorException(
                    responseMessage,
                    randomExceptionMessage);

            var expectedDependencyValidationException = 
                new TeacherDependencyValidationException(internalServerErrorException);

            this.apiBrokerMock.Setup(apiBroker => 
                apiBroker.GetAllTeachersAsync())
                .Throws(internalServerErrorException);

            var retrieveAllTeachersTask =
                teacherService.RetrieveAllTeachersAsync();

            await Assert.ThrowsAsync<TeacherDependencyValidationException>(() => 
                retrieveAllTeachersTask.AsTask());

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(loggingBroker =>
                loggingBroker.LogError(It.Is(SameValidationExceptionAs(expectedDependencyValidationException))));

            apiBrokerMock.VerifyNoOtherCalls();
            loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfErrorOccursAndLogItAsync()
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
