// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Teachers.Exceptions
{
    public class InvalidTeacherException : Exception
    {
        public InvalidTeacherException(string parameterName, object parameterValue)
            : base("Invalid Teacher error occurred, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
