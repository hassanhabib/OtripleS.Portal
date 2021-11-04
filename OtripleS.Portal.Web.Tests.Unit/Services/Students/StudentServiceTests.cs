// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using OtripleS.Portal.Web.Brokers.API;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Services.Students;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Students
{
    public partial class StudentServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentService studentService;

        public StudentServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentService = new StudentService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private List<Student> CreateRandomStudents() =>
           CreateStudentFiller().Create(count: GetRandomNumber()).ToList();

        private static Expression<Func<Exception, bool>> SameExceptionAs(
            Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static string GetRandomString() => new MnemonicString().GetValue();

        private static Filler<Student> CreateStudentFiller()
        {
            var filler = new Filler<Student>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
