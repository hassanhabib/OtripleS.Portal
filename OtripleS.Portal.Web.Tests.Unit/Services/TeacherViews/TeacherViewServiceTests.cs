// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Services.Teachers;
using OtripleS.Portal.Web.Services.TeacherViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        private readonly Mock<ITeacherService> teacherServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITeacherViewService teacherViewService;

        public TeacherViewServiceTests()
        {
            this.teacherServiceMock = new Mock<ITeacherService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.teacherViewService = 
                new TeacherViewService(
                    teacherService: teacherServiceMock.Object,
                    loggingBroker: loggingBrokerMock.Object);
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private List<Teacher> CreateRandomTeachers() =>
            CreateTeacherFiller().Create(count: GetRandomNumber()).ToList();

        private static Expression<Func<Exception, bool>> SameExceptionAs(
            Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static Filler<Teacher> CreateTeacherFiller()
        {
            var filler = new Filler<Teacher>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(valueToUse: GetRandomDateTime());

            return filler;
        }
    }
}
