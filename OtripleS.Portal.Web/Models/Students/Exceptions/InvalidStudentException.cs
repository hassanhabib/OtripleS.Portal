// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Students.Exceptions
{
    public class InvalidStudentException : Exception
    {
        public InvalidStudentException(string parameterName, object parameterValue)
            : base("Invalid Student error occurred, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
