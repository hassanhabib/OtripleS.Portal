using System;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class CourseServiceException : Exception
    {
        public CourseServiceException(Exception innerException)
            : base(message: "Course service error occurred, contact support.", innerException) { }
    }
}
