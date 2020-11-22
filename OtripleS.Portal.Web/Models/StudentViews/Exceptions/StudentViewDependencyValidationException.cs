// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class StudentViewDependencyValidationException : Exception
    {
        public StudentViewDependencyValidationException(Exception innerException)
            : base("Student view dependency validation error occurred, try again.", innerException) { }
    }
}
