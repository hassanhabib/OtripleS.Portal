// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldLogWarningOnRetrieveAllWhenTeachersWereEmpty()
        {
            IList<Teacher> emptyTeacherCollection = Enumerable.Empty<Teacher>().ToList();
            var expectedTeachersFromApi = emptyTeacherCollection;

            apiBrokerMock.Setup(apiBroker =>
                apiBroker.GetAllTeachersAsync())
                .ReturnsAsync(expectedTeachersFromApi);

            var retrievedTeachers = 
                await this.teacherService.RetrieveAllTeachersAsync();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachersFromApi);

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(x => 
                x.LogWarning("No teachers retrieved from the api."), Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
