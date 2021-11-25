// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
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
        public async Task ShouldRetrieveTeacherViewsAsync()
        {
            List<dynamic> dynamicTeacherViewPropertiesCollection =
                CreateRandomTeacherViewCollections();

            List<Teacher> randomTeachers =
                dynamicTeacherViewPropertiesCollection.Select(property =>
                    new Teacher
                    {
                        Id = property.Id,
                        UserId = property.UserId,
                        EmployeeNumber = property.EmployeeNumber,
                        FirstName = property.FirstName,
                        MiddleName = property.MiddleName,
                        LastName = property.LastName,
                        Gender = property.Gender,
                        Status = property.Status,
                        CreatedDate = property.CreatedDate,
                        UpdatedDate = property.UpdatedDate,
                        CreatedBy = property.CreatedBy,
                        UpdatedBy = property.UpdatedBy
                    }).ToList();

            List<Teacher> retrievedTeachers = randomTeachers;

            List<TeacherView> randomTeacherViews =
                dynamicTeacherViewPropertiesCollection.Select(property =>
                    new TeacherView
                    {
                        EmployeeNumber = property.EmployeeNumber,
                        FirstName = property.FirstName,
                        MiddleName = property.MiddleName,
                        LastName = property.LastName,
                        Gender = (TeacherGenderView)property.Gender,
                        Status = (TeacherStatusView)property.Status
                    }).ToList();

            List<TeacherView> expectedTeacherViews = randomTeacherViews;

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ReturnsAsync(retrievedTeachers);

            List<TeacherView> retrievedTeacherViews =
                await this.teacherViewService.RetrieveAllTeacherViewsAsync();

            retrievedTeacherViews.Should().BeEquivalentTo(expectedTeacherViews);

            this.teacherServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once());

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
