// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using OtripleS.Portal.Web.Brokers.DateTimes;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Brokers.Navigations;
using OtripleS.Portal.Web.Models.Genders;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Students;
using OtripleS.Portal.Web.Services.StudentViews;
using OtripleS.Portal.Web.Services.Users;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.StudentViews
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

        private static dynamic CreateRandomStudentViewProperties(
            DateTimeOffset auditDates,
            Guid auditIds)
        {
            Gender randomStudentGender = GetRandomGender();

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

        private static Gender GetRandomGender()
        {
            int studentGenderCount =
                Enum.GetValues(typeof(Gender)).Length;

            int randomStudentGenderValue =
                new IntRange(min: 0, max: studentGenderCount).GetValue();

            return (Gender)randomStudentGenderValue;
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
