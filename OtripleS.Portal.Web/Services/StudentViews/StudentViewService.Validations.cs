// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;

namespace OtripleS.Portal.Web.Services.StudentViews
{
    public partial class StudentViewService
    {
        private static void ValidateStudentView(StudentView studentView)
        {
            if (studentView is null)
            { 
                throw new NullStudentViewException();
            }
        }

        private static void ValidateRoute(string route)
        {
            if (IsInvalid(route))
            {
                throw new InvalidStudentViewException(
                    parameterName: "Route",
                    parameterValue: route);
            }
        }

        private static bool IsInvalid(string text) => String.IsNullOrWhiteSpace(text);
    }
}
