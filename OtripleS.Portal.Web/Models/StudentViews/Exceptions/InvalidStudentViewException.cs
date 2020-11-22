// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.StudentViews.Exceptions
{
    public class InvalidStudentViewException : Exception
    {
        public InvalidStudentViewException(string parameterName, string parameterValue)
            : base($"Invalid Student View error occured. " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}")
        { }
    }
}
