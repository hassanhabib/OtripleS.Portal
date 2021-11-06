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
        public async Task ShouldRetrieveTeacherViewsAsync()
        {
            var randomUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDateTime();

            dynamic dynamicTeacherProperties =
                CreateRandomTeacherProperties(
                    auditDates: randomDateTime,
                    auditIds: randomUserId);

            var teacher = new Teacher
            {
                Id = dynamicTeacherProperties.Id,
                UserId = dynamicTeacherProperties.UserId,
                EmployeeNumber = dynamicTeacherProperties.EmployeeNumber,
                FirstName = dynamicTeacherProperties.FirstName,
                MiddleName = dynamicTeacherProperties.MiddleName,
                LastName = dynamicTeacherProperties.LastName,
                Gender = dynamicTeacherProperties.Gender,
                Status = dynamicTeacherProperties.Status,
                CreatedDate = randomDateTime,
                UpdatedDate = randomDateTime,
                CreatedBy = randomUserId,
                UpdatedBy = randomUserId
            };

            var teacherView = new TeacherView
            {
                EmployeeNumber = dynamicTeacherProperties.EmployeeNumber,
                FirstName = dynamicTeacherProperties.FirstName,
                MiddleName = dynamicTeacherProperties.MiddleName,
                LastName = dynamicTeacherProperties.LastName,
                Gender = (TeacherGenderView)dynamicTeacherProperties.Gender,
                Status = (TeacherStatusView)dynamicTeacherProperties.Status
            };

            var randomTeachers = new List<Teacher> { teacher };
            var retrievedServiceTeachers = randomTeachers;
            var expectedTeacherViews = new List<TeacherView> { teacherView };

            this.teacherServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ReturnsAsync(retrievedServiceTeachers);

            List<TeacherView> retrievedTeacherViews =
                await this.teacherViewService.RetrieveAllTeachersAsync();

            retrievedTeacherViews.Should().BeEquivalentTo(expectedTeacherViews);

            this.teacherServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once());

            this.teacherServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
