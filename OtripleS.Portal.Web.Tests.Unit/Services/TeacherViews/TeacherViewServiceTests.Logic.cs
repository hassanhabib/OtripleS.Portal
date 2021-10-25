// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.TeacherViews;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveTeachersViews()
        {
            List<Teacher> randomTeachers = CreateRandomTeachers();
            List<Teacher> retrievedServiceTeachers = randomTeachers;
            List<TeacherView> expectedTeachers = 
                CreateExpectedTeachersViews(retrievedServiceTeachers);

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ReturnsAsync(retrievedServiceTeachers);

            IReadOnlyList<TeacherView> retrievedTeachers =
                await this.teacherViewService.RetrieveAllTeachers();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachers);

            this.teacherServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once());

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
