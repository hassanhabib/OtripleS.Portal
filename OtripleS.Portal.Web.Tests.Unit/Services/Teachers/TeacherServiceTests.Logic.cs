// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllTeachersAsync()
        {
            IReadOnlyList<Teacher> randomTeachers = CreateRandomTeachers();
            IReadOnlyList<Teacher> apiTeachers = randomTeachers;
            IReadOnlyList<Teacher> expectedTeachers = apiTeachers;

            this.apiBrokerMock.Setup(apiBroker =>
                apiBroker.GetAllTeachersAsync())
                .ReturnsAsync(apiTeachers);

            IReadOnlyList<Teacher> retrievedTeachers =
                await teacherService.RetrieveAllTeachersAsync();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachers);

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(),
                Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
