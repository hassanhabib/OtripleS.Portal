// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OtripleS.Portal.Web.Brokers.DateTimes;
using OtripleS.Portal.Web.Brokers.Logging;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Students;
using OtripleS.Portal.Web.Services.Users;

namespace OtripleS.Portal.Web.Services.StudentViews
{
    public class StudentViewService : IStudentViewService
    {
        private readonly IStudentService studentService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentViewService(
            IStudentService studentService, 
            IUserService userService, 
            IDateTimeBroker dateTimeBroker, 
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<StudentView> AddStudentViewAsync(StudentView studentView)
        {
            throw new System.NotImplementedException();
        }
    }
}
