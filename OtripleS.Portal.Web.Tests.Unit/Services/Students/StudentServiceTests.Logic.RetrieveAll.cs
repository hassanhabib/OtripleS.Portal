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
            List<Student> rerievedStudents = randomStudents;
            List<Student> expetedStudents = rerievedStudents.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllStudentsAsync())
                    .ReturnsAsync(rerievedStudents);

            // when
            List<Student> actualStudents =
                await this.studentService.RetrieveAllStudentsAsync();

            // then
            actualStudents.Should().BeEquivalentTo(expetedStudents);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllStudentsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
