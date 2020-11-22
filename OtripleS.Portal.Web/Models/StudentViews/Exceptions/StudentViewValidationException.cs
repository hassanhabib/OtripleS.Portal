// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class StudentViewValidationException : Exception
    {
        public StudentViewValidationException(Exception innerException)
            : base($"Student View validation error occurred, try again.", innerException) { }
    }
}
