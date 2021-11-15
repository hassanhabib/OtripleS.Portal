// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task ShouldRetrieveAllStudentViewsAsync()
        {
            // given
            var randomUserId = Guid.NewGuid();
            DateTimeOffset randomDateTime = GetRandomDate();

            List<dynamic> dynamicStudentViewPropertiesCollection =
                CreateRandomStudentViewCollections(
                    auditDates: randomDateTime,
                    auditIds: randomUserId);

            List<Student> randomStudents = 
                dynamicStudentViewPropertiesCollection.Select(property =>
                new Student
                {
                    Id = property.Id,
                    UserId = property.UserId,
                    IdentityNumber = property.IdentityNumber,
                    FirstName = property.FirstName,
                    MiddleName = property.MiddleName,
                    LastName = property.LastName,
                    Gender = (StudentGender) property.Gender,
                    BirthDate = property.BirthDate,
                    CreatedDate = randomDateTime,
                    UpdatedDate = randomDateTime,
                    CreatedBy = randomUserId,
                    UpdatedBy = randomUserId
                }).ToList();

            List<Student> retrievedStudents = randomStudents;

            List<StudentView> randomStudentViews = 
                dynamicStudentViewPropertiesCollection.Select(property => 
                new StudentView
                {
                    IdentityNumber = property.IdentityNumber,
                    FirstName = property.FirstName,
                    MiddleName = property.MiddleName,
                    LastName = property.LastName,
                    Gender = (StudentViewGender)property.Gender,
                    BirthDate = property.BirthDate
                }).ToList();

            List<StudentView> expectedStudentViews = randomStudentViews;

            this.studentServiceMock.Setup(service =>
                service.RetrieveAllStudentsAsync())
                    .ReturnsAsync(retrievedStudents);

            // when
            List<StudentView> retrievedStudentViews =
                await this.studentViewService.RetrieveAllStudentViewsAsync();

            // then
            retrievedStudentViews.Should().BeEquivalentTo(expectedStudentViews);

            this.studentServiceMock.Verify(service =>
                service.RetrieveAllStudentsAsync(),
                    Times.Once());

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.navigationBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
        }
    }
}
