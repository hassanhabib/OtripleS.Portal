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
            switch (studentView)
            {
                case null:
                    throw new NullStudentViewException();

                case { } when IsInvalid(studentView.FirstName):
                    throw new InvalidStudentViewException(
                        parameterName: nameof(StudentView.FirstName),
                        parameterValue: studentView.FirstName);

                case { } when IsInvalid(studentView.BirthDate):
                    throw new InvalidStudentViewException(
                        parameterName: nameof(StudentView.BirthDate),
                        parameterValue: studentView.BirthDate);
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
        private static bool IsInvalid(DateTimeOffset date) => date == default;
    }
}
