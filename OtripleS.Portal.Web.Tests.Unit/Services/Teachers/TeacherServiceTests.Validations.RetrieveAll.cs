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
using Force.DeepCloner;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldLogWarningOnRetrieveAllWhenTeachersWereEmpty()
        {
            IReadOnlyList<Teacher> emptyTeacherCollection = 
                Enumerable.Empty<Teacher>().ToList();

            IReadOnlyList<Teacher> apiTeachers = emptyTeacherCollection;
            IReadOnlyList<Teacher> expectedTeachers = apiTeachers.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllTeachersAsync())
                    .ReturnsAsync(apiTeachers);

            IReadOnlyList<Teacher> retrievedTeachers = 
                await this.teacherService.RetrieveAllTeachersAsync();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachers);

            this.apiBrokerMock.Verify(broker => 
                broker.GetAllTeachersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogWarning("No teachers retrieved from the api."), 
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
