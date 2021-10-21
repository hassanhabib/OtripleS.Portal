// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace OtripleS.Portal.Web.Models.Teachers.Exceptions
{
      public class TeacherValidationException : Xeption
      {
            public TeacherValidationException(Exception innerException)
                : base(message: "Teacher validation error occurred, try again.", innerException) { }
      }
}
