// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class StudentViewDependencyException : Exception
    {
        public StudentViewDependencyException(Exception innerException)
            : base("Student view dependency error occurred, contact support.", innerException) { }
    }
}
