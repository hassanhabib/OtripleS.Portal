// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class StudentValidationException : Exception
    {
        public StudentValidationException(Exception innerException)
            : base("Student validation error occurred, try again.", innerException) { }
    }
}
