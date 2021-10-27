// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.Teachers.Exceptions;
using OtripleS.Portal.Web.Services.Teachers;
using OtripleS.Portal.Web.Services.TeacherViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Exception();

            var teacherServiceDependencyException =
                new TeacherDependencyException(innerException);

            var teacherServiceException =
                new TeacherServiceException(innerException);

            return new TheoryData<Exception>
            {
                teacherServiceDependencyException,
                teacherServiceException
            };
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomName() => 
            new RealNames(NameStyle.FirstName).GetValue();

        private static string GetRandomString() => new MnemonicString().GetValue();

        private static T GetRandomEnumValue<T>()
        {
            int enumValuesCount =
                Enum.GetValues(typeof(T)).Length;

            int randomEnumValue =
                new IntRange(min: 0, max: enumValuesCount).GetValue();

            return (T)(object)randomEnumValue;
        }

        private static dynamic CreateRandomTeacherProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            return new
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid().ToString(),
                EmployeeNumber = GetRandomString(),
                FirstName = GetRandomName(),
                MiddleName = GetRandomName(),
                LastName = GetRandomName(),
                Gender = GetRandomEnumValue<TeacherGender>(),
                Status = GetRandomEnumValue<TeacherStatus>(),
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                CreatedBy = auditIds,
                UpdateBy = auditIds
            };
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(
            Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
    }
}
