// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Teachers.Exceptions
{
    public class TeacherServiceException : Exception
    {
        public TeacherServiceException(Exception innerException)
            : base("Teacher service error occurred, contact support.", innerException) { }
    }
}
