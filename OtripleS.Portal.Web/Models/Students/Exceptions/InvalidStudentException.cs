// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class InvalidStudentException : Xeption
    {
        public InvalidStudentException()
            : base(message: "Invalid stuent error occurred, please fix the errors and try again.")
        { }
    }
}
