// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
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
            var randomUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDateTime();

            dynamic dynamicTeacherViewProperties = 
                CreateRandomTeacherProperties(
                    auditDates: randomDateTime,
                    auditIds: randomUserId);

            var teacher = new Teacher
            {
                Id = dynamicTeacherViewProperties.Id,
                UserId = dynamicTeacherViewProperties.UserId,
                EmployeeNumber = dynamicTeacherViewProperties.EmployeeNumber,
                FirstName = dynamicTeacherViewProperties.FirstName,
                MiddleName = dynamicTeacherViewProperties.MiddleName,
                LastName = dynamicTeacherViewProperties.LastName,
                Gender = dynamicTeacherViewProperties.Gender,
                Status = dynamicTeacherViewProperties.Status,
                CreatedDate = randomDateTime,
                UpdatedDate = randomDateTime,
                CreatedBy = randomUserId,
                UpdatedBy = randomUserId
            };

            var teacherView = new TeacherView
            {
                EmployeeNumber = dynamicTeacherViewProperties.EmployeeNumber,
                FirstName = dynamicTeacherViewProperties.FirstName,
                MiddleName = dynamicTeacherViewProperties.MiddleName,
                LastName = dynamicTeacherViewProperties.LastName,
                Gender = (TeacherGenderView)dynamicTeacherViewProperties.Gender,
                Status = (TeacherStatusView)dynamicTeacherViewProperties.Status
            };

            var randomTeachers = new List<Teacher> { teacher };
            var retrievedServiceTeachers = randomTeachers;
            var expectedTeachers = new List<TeacherView> { teacherView };

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ReturnsAsync(retrievedServiceTeachers);

            List<TeacherView> retrievedTeachers =
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
