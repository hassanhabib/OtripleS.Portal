// ---------------------------------------------------------------
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
        public async Task ShouldRetrieveAllStudentsAsync()
        {
            // given
            List<Student> randomStudents = CreateRandomStudents();
            List<Student> apiStudents = randomStudents;
            List<Student> expetedStudents = apiStudents.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ReturnsAsync(apiStudents);

            // when
            List<Student> retrievedStudents = 
                await this.studentService.RetrieveAllStudentsAsync();

            // then
            retrievedStudents.Should().BeEquivalentTo(expetedStudents);

            this.apiBrokerMock.Verify(broker => 
                broker.GetAllStudentsAsync(), 
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
