// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class FailedStudentViewServiceException : Xeption
    {
        public FailedStudentViewServiceException(Exception innerException)
            : base(message: "Failed student view service error occurred, please contact support", innerException)
        { }
    }
}
