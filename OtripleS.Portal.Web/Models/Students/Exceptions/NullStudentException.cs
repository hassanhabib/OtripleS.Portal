// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class NullStudentException : Exception
    {
        public NullStudentException()
            : base("Null student error occurred.") { }
    }
}
