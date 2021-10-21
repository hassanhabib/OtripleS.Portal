// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OtripleS.Portal.Web.Brokers.API;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Services.Teachers;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly TeacherService teacherService;

        public TeacherServiceTests()
        {
            apiBrokerMock = new Mock<IApiBroker>();
            loggingBrokerMock = new Mock<ILoggingBroker>();

            teacherService = new TeacherService(
                apiBroker: apiBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }
        
        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private IList<Teacher> CreateRandomTeachers() =>
            CreateTeacherFiller().Create(GetRandomNumber()).ToList();

        private static Filler<Teacher> CreateTeacherFiller()
        {
            var filler = new Filler<Teacher>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
