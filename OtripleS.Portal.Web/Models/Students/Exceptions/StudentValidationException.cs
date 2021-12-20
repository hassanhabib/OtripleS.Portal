// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class StudentValidationException : Xeption
    {
        public StudentValidationException(Exception innerException)
            : base(message: "Student validation error occurred, try again.", innerException) { }
    }
}
