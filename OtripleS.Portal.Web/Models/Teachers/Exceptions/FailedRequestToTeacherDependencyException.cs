// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Teachers.Exceptions
{
    public class FailedRequestToTeacherDependencyException : Xeption
    {
        public FailedRequestToTeacherDependencyException(Exception innerException)
            : base(message: "Failed request to teacher dependency error occurred, please contact support.", innerException)
        { }
    }
}
