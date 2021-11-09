// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.StudentViews
{
    public partial class StudentViewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllStudentsViewAsync()
        {
            // given
            var randomUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDate();

            dynamic dynamicStudentProperties =
                CreateRandomStudentViewProperties(
                    auditDates: randomDateTime,
                    auditIds: randomUserId);

            var student = new Student
            {
                Id = dynamicStudentProperties.Id,
                UserId = dynamicStudentProperties.UserId,
                IdentityNumber = dynamicStudentProperties.IdentityNumber,
                FirstName = dynamicStudentProperties.FirstName,
                MiddleName = dynamicStudentProperties.MiddleName,
                LastName = dynamicStudentProperties.LastName,
                Gender = (StudentGender)dynamicStudentProperties.Gender,
                BirthDate = dynamicStudentProperties.BirthDate,
                CreatedDate = randomDateTime,
                UpdatedDate = randomDateTime,
                CreatedBy = randomUserId,
                UpdatedBy = randomUserId
            };

            var studentView = new StudentView
            {
                IdentityNumber = dynamicStudentProperties.IdentityNumber,
                FirstName = dynamicStudentProperties.FirstName,
                MiddleName = dynamicStudentProperties.MiddleName,
                LastName = dynamicStudentProperties.LastName,
                Gender = (StudentViewGender)dynamicStudentProperties.Gender,
                BirthDate = dynamicStudentProperties.BirthDate
            };

            var randomStudents = new List<Student> { student };
            var actualStudentViews = randomStudents;
            var expectedStudentViews = new List<StudentView> { studentView };

            this.studentServiceMock.Setup(service =>
                service.RetrieveAllStudentsAsync())
                    .ReturnsAsync(actualStudentViews);

            // when
            List<StudentView> retrievedStudentViews =
                await this.studentViewService.RetrieveAllStudentViewsAsync();

            // then
            retrievedStudentViews.Should().BeEquivalentTo(expectedStudentViews);

            this.studentServiceMock.Verify(service =>
                service.RetrieveAllStudentsAsync(),
                    Times.Once());

            this.studentServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
        }
    }
}
