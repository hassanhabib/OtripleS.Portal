// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Moq;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Services.Teachers;
using OtripleS.Portal.Web.Services.TeacherViews;
using System;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Services.TeacherViews
{
    public partial class TeacherViewServiceTests
    {
        private readonly Mock<ITeacherService> teacherServiceMock;
        private readonly ITeacherViewService teacherViewService;

        public TeacherViewServiceTests()
        {
            this.teacherServiceMock = new Mock<ITeacherService>();
            this.teacherViewService = 
                new TeacherViewService(teacherService: teacherServiceMock.Object);
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private List<Teacher> CreateRandomTeachers() =>
            CreateTeacherFiller().Create(count: GetRandomNumber()).ToList();

        private static Filler<Teacher> CreateTeacherFiller()
        {
            var filler = new Filler<Teacher>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(valueToUse: GetRandomDateTime());

            return filler;
        }
    }
}
