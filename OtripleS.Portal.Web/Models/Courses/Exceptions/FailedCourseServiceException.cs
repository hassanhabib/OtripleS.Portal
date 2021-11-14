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
