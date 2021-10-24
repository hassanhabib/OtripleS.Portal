// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.TeacherViews;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                retrievedServiceTeachers.Select(retrievedServiceTeacher =>
                    new TeacherView
                    {
                        EmployeeNumber = retrievedServiceTeacher.EmployeeNumber,
                        FirstName = retrievedServiceTeacher.FirstName,
                        MiddleName = retrievedServiceTeacher.MiddleName,
                        LastName = retrievedServiceTeacher.LastName,
                        Gender = (TeacherGenderView)retrievedServiceTeacher.Gender,
                        Status = (TeacherStatusView)retrievedServiceTeacher.Status,
                    }).ToList();

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
        }
    }
}
