// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentRegistrationComponents.Exceptions
{
    public class StudentRegistrationComponentException : Exception
    {
        public StudentRegistrationComponentException(Exception innerException)
            : base("Error occurred, contact support", innerException)
        {

        }
    }
}
