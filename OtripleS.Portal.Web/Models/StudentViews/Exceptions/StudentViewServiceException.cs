// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class StudentViewServiceException : Exception
    {
        public StudentViewServiceException(Exception innerException)
            : base("Student View service error occured, contact support.", innerException) { }
    }
}
