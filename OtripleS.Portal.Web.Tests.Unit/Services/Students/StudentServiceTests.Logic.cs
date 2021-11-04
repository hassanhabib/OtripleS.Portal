﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OtripleS.Portal.Web.Models.Students;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldRegisterStudentAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student inputStudent = randomStudent;
            Student retrievedStudent = inputStudent;
            Student expectedStudent = retrievedStudent;

            this.apiBrokerMock.Setup(broker =>
                broker.PostStudentAsync(inputStudent))
                    .ReturnsAsync(retrievedStudent);

            // when
            Student actualStudent =
                await this.studentService
                    .RegisterStudentAsync(inputStudent);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.apiBrokerMock.Verify(broker =>
                broker.PostStudentAsync(inputStudent),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllStudentsAsync()
        {
            List<Student> randomStudents = CreateRandomStudents();
            List<Student> apiStudents = randomStudents;
            List<Student> expectedStudents = apiStudents.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ReturnsAsync(apiStudents);

            List<Student> retrievedStudents =
                await studentService.RetrieveAllStudentsAsync();

            retrievedStudents.Should().BeEquivalentTo(expectedStudents);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllStudentsAsync(),
                    Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
