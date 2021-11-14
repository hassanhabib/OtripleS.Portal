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
