// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class CourseServiceException : Exception
    {
        public CourseServiceException(Exception innerException)
            : base(message: "Course service error occurred, contact support.", innerException) { }
    }
}
