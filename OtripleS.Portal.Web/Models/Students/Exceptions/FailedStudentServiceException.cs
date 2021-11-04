using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class FailedStudentServiceException : Xeption
    {
        public FailedStudentServiceException(Exception innerException)
            : base("Failed student service occurred, please contact support",innerException)
        { }
    }
}
