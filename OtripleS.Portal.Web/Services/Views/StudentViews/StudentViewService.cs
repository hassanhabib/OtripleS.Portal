// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.DateTimes;
using OtripleS.Portal.Web.Brokers.Loggings;
using OtripleS.Portal.Web.Brokers.Navigations;
using OtripleS.Portal.Web.Brokers.Stores;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Foundations.Students;
using OtripleS.Portal.Web.Services.Foundations.Users;

namespace OtripleS.Portal.Web.Services.Views.StudentViews
{
    public partial class StudentViewService : IStudentViewService
    {
        private readonly IStudentService studentService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly INavigationBroker navigationBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IStateStoreBroker stateStoreBroker;

        public StudentViewService(
            IStudentService studentService,
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            INavigationBroker navigationBroker,
            ILoggingBroker loggingBroker,
            IStateStoreBroker stateStoreBroker)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.navigationBroker = navigationBroker;
            this.loggingBroker = loggingBroker;
            this.stateStoreBroker = stateStoreBroker;
        }

        public ValueTask<StudentView> AddStudentViewAsync(StudentView studentView) =>
        TryCatch(async () =>
        {
            ValidateStudentView(studentView);
            Student student = MapToStudent(studentView);
            await this.studentService.AddStudentAsync(student);
            this.stateStoreBroker.StudentAdded();

            return studentView;
        });

        public void NavigateTo(string route) =>
        TryCatch(() =>
        {
            ValidateRoute(route);
            this.navigationBroker.NavigateTo(route);
        });

        public ValueTask<List<StudentView>> RetrieveAllStudentViewsAsync() =>
        TryCatch(async () =>
        {
            List<Student> students =
                await this.studentService.RetrieveAllStudentsAsync();

            return students.Select(AsStudentView).ToList();
        });

        private Student MapToStudent(StudentView studentView)
        {
            Guid currentLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTime();

            return new Student
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid().ToString(),
                IdentityNumber = studentView.IdentityNumber,
                FirstName = studentView.FirstName,
                MiddleName = studentView.MiddleName,
                LastName = studentView.LastName,
                Gender = (StudentGender)studentView.Gender,
                BirthDate = studentView.BirthDate,
                CreatedBy = currentLoggedInUserId,
                UpdatedBy = currentLoggedInUserId,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime
            };
        }

        public async Task<StudentView> RetrieveOlderStudentdViewAsync()
        {
            var students = await RetrieveAllStudentViewsAsync();

            var olderStudent =  students.OrderBy(studentView => studentView.BirthDate)
                .FirstOrDefault();

            return olderStudent;
        }

        private static Func<Student, StudentView> AsStudentView =>
            student => new StudentView
            {
                IdentityNumber = student.IdentityNumber,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Gender = (StudentViewGender)student.Gender,
            };
    }
}
