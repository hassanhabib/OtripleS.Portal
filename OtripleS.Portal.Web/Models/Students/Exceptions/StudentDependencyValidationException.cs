// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class StudentDependencyValidationException : Exception
    {
        public StudentDependencyValidationException(Exception innerException)
            : base("Student depdendencyvalidation error occurred, try again.", innerException) { }
    }
}
