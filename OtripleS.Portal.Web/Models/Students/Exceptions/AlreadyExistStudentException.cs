// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class AlreadyExistStudentException : Xeption
    {
        public AlreadyExistStudentException(Exception innerException)
            : base(message: "Error occurred, co",
                  innerException)
        { }
    }
}
