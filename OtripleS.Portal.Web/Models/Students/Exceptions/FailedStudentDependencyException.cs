using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class FailedStudentDependencyException : Xeption
    {
        public FailedStudentDependencyException(Exception innerException)
            : base("Failed student dependency error occurred, please contact support.", innerException)
        { }
    }
}
