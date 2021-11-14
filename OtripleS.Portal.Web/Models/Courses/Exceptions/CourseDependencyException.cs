// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class CourseDependencyException : Exception
    {
        public CourseDependencyException(Exception innerException)
            : base(message: "Course dependency error occurred, contact support.", innerException) { }
    }
}
