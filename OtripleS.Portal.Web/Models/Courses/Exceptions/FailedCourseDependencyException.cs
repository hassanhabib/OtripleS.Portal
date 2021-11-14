// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class FailedCourseDependencyException : Xeption
    {
        public FailedCourseDependencyException(Exception innerException)
            : base(message: "Failed course dependency error occurred, please contact support.", innerException)
        { }
    }
}
