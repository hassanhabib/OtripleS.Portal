// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Teachers.Exceptions
{
    public class FailedTeacherDependencyException : Xeption
    {
        public FailedTeacherDependencyException(Exception innerException)
            : base(message: "Failed teacher dependency error occurred, please contact support.", innerException)
        { }
    }
}
