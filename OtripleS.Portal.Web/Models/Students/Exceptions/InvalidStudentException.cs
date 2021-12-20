// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class InvalidStudentException : Xeption
    {
        public InvalidStudentException()
            : base(message: "Invalid student error occurred, please fix the errors and try again.")
        { }

        public InvalidStudentException(Exception innerException)
            : base(message: "Invalid student error occurred, please fix the errors and try again.",
                  innerException,
                  innerException.Data)
        { }
    }
}
