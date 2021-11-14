using System;

namespace OtripleS.Portal.Web.Models.Courses.Exceptions
{
    public class CourseDependencyException : Exception
    {
        public CourseDependencyException(Exception innerException)
            : base(message: "Course dependency error occurred, contact support.", innerException) { }
    }
}
