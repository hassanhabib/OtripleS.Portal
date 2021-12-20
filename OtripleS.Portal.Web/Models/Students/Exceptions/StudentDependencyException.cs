// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class StudentDependencyException : Xeption
    {
        public StudentDependencyException(Exception innerException)
            : base(message: "Student dependency error occurred, contact support.", innerException) { }
    }
}
