// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using OtripleS.Portal.Web.Brokers.DateTimes;
using OtripleS.Portal.Web.Brokers.Loggings;
using OtripleS.Portal.Web.Brokers.Navigations;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Foundations.Students;
using OtripleS.Portal.Web.Services.Views.StudentViews;
using OtripleS.Portal.Web.Services.Foundations.Users;
using Tynamix.ObjectFiller;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Services.Views.StudentViews
{
    public partial class StudentViewServiceTests
    {
        private readonly Mock<IStudentService> studentServiceMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<INavigationBroker> navigationBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IStudentViewService studentViewService;

        public StudentViewServiceTests()
        {
            this.studentServiceMock = new Mock<IStudentService>();
            this.userServiceMock = new Mock<IUserService>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.navigationBrokerMock = new Mock<INavigationBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<Student>(student => student.Id);
            compareConfig.IgnoreProperty<Student>(student => student.UserId);
            this.compareLogic = new CompareLogic(compareConfig);

            this.studentViewService = new StudentViewService(
                studentService: this.studentServiceMock.Object,
                userService: this.userServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                navigationBroker: this.navigationBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Exception();

            var studentDependencyException =
                new StudentDependencyException(innerException);

            var studentServiceException =
                new StudentServiceException(innerException);

            return new TheoryData<Exception>
            {
                studentDependencyException,
                studentServiceException
            };
        }

        private static dynamic CreateRandomStudentViewProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            StudentGender randomStudentGender = GetRandomGender();

            return new
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid().ToString(),
                IdentityNumber = GetRandomString(),
                FirstName = GetRandomName(),
                MiddleName = GetRandomName(),
                LastName = GetRandomName(),
                BirthDate = GetRandomDate(),
                Gender = randomStudentGender,
                GenderView = (StudentViewGender)randomStudentGender,
                CreatedDate = auditDates,
                UpdatedDate = auditDates,
                CreatedBy = auditIds,
                UpdatedBy = auditIds
            };
        }

        private static List<dynamic> CreateRandomStudentViewCollections()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(0, randomCount).Select(item =>
            {
                StudentGender studentGender = GetRandomGender();

                return new
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid().ToString(),
                    IdentityNumber = GetRandomString(),
                    FirstName = GetRandomName(),
                    MiddleName = GetRandomName(),
                    LastName = GetRandomName(),
                    BirthDate = GetRandomDate(),
                    Gender = studentGender,
                    GenderView = (StudentViewGender)studentGender,
                    CreatedDate = GetRandomDate(),
                    UpdatedDate = GetRandomDate(),
                    CreatedBy = Guid.NewGuid(),
                    UpdatedBy = Guid.NewGuid()
                };
            }).ToList<dynamic>();
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private Expression<Func<Student, bool>> SameStudentAs(Student expectedStudent)
        {
            return actualStudent => this.compareLogic.Compare(expectedStudent, actualStudent)
                .AreEqual;
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentView CreateRandomStudentView() =>
            CreateStudentViewFiller().Create();

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static string GetRandomRoute() =>
            new RandomUrl().GetValue();

        private static string GetRandomName() =>
            new RealNames(NameStyle.FirstName).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static StudentGender GetRandomGender()
        {
            int studentGenderCount =
                Enum.GetValues(typeof(StudentGender)).Length;

            int randomStudentGenderValue =
                new IntRange(min: 0, max: studentGenderCount).GetValue();

            return (StudentGender)randomStudentGenderValue;
        }

        private static Filler<StudentView> CreateStudentViewFiller()
        {
            var filler = new Filler<StudentView>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
