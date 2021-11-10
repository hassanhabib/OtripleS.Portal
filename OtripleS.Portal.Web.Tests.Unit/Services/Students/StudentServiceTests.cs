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
using OtripleS.Portal.Web.Brokers.Apis;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Services.Students;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xunit;

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
        public static TheoryData ValidationApiException()
        {
            string exceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseConflictException =
                new HttpResponseConflictException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseBadRequestException,
                httpResponseConflictException
            };
        }

        public static TheoryData CriticalApiException()
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

        public static TheoryData DependencyApiException()
        {
            string exceptionMessage = GetRandomString();
            var responseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseInternalServerErrorException =
                new HttpResponseInternalServerErrorException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseException,
                httpResponseInternalServerErrorException
            };
        }


        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static List<Student> CreateRandomStudents() =>
            CreateStudentFiller().Create(count: GetRandomNumber()).ToList();

        private static Expression<Func<Exception, bool>> SameExceptionAs(
            Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static string GetRandomString() => new MnemonicString().GetValue();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Student> CreateStudentFiller()
        {
            var filler = new Filler<Student>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
