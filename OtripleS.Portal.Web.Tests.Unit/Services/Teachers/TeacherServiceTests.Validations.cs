﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using OtripleS.Portal.Web.Models.Teachers;
using Force.DeepCloner;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        [Fact]
        public async Task ShouldLogWarningOnRetrieveAllWhenTeachersWereEmpty()
        {
            IList<Teacher> emptyTeacherCollection = Enumerable.Empty<Teacher>().ToList();
            IList<Teacher> apiTeachers = emptyTeacherCollection;
            IList<Teacher> expectedTeachers = apiTeachers.DeepClone();

            apiBrokerMock.Setup(apiBroker =>
                apiBroker.GetAllTeachersAsync())
                .ReturnsAsync(expectedTeachers);

            var retrievedTeachers = 
                await this.teacherService.RetrieveAllTeachersAsync();

            retrievedTeachers.Should().BeEquivalentTo(expectedTeachers);

            this.apiBrokerMock.Verify(apiBroker => 
                apiBroker.GetAllTeachersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(x => 
                x.LogWarning("No teachers retrieved from the api."), Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}