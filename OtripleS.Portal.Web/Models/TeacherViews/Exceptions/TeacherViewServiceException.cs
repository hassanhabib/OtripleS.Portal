// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.TeacherViews.Exceptions
{
    public class TeacherViewServiceException : Xeption
    {
        public TeacherViewServiceException(Exception innerException)
            : base(message: "Teacher view service error occurred, please contact support.", innerException)
        { }
    }
}
