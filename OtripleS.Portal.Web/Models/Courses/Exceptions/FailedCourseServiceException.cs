// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class FailedCourseServiceException : Xeption
    {
        public FailedCourseServiceException(Exception innerException)
            : base(message: "Failed course service occurred, please contact support", innerException)
        { }
    }
}
