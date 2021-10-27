// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

namespace OtripleS.Portal.Web.Models.TeacherViews
{
    public class TeacherView
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public TeacherGenderView Gender { get; set; }
        public TeacherStatusView Status { get; set; }
    }
}
