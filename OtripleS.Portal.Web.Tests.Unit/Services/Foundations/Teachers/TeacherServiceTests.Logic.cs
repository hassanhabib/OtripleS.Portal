// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Foundations.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllTeachersAsync()
        {
            List<Teacher> randomTeachers = CreateRandomTeachers();
            List<Teacher> apiTeachers = randomTeachers;
            List<Teacher> expectedTeachers = apiTeachers.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ReturnsAsync(apiTeachers);

            List<Teacher> retrievedTeachers =
                await teacherService.RetrieveAllTeachersAsync();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachers);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllTeachersAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
