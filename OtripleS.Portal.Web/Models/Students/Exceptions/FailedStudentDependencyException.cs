// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class FailedStudentDependencyException : Xeption
    {
        public FailedStudentDependencyException(Exception innerException)
            : base(message: "Failed student dependency error occurred, please contact support.", innerException)
        { }
    }
}
