// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using Moq;
using OtripleS.Portal.Web.Brokers.API;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Services.Teachers;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Teachers
{
    public partial class TeacherServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly TeacherService teacherService;

        public TeacherServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.teacherService = new TeacherService(
                apiBroker: apiBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyExceptions()
        {
            string exceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseUnAuthorizedException =
                new HttpResponseUnauthorizedException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpResponseUrlNotFoundException,
                httpResponseUnAuthorizedException
            };
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static string GetRandomString() => new MnemonicString().GetValue();

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

        private static Expression<Func<Exception, bool>> SameValidationExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
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
