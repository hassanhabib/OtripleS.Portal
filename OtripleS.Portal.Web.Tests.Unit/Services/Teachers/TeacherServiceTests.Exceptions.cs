// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using RESTFulSense.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;
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
                apiBroker.GetAllTeachersAsync)
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
    }
}
