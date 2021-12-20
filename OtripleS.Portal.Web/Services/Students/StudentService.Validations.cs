// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;

namespace OtripleS.Portal.Web.Services.Students
{
    public partial class StudentService
    {
        private void ValidateStudent(Student student)
        {
            switch (student)
            {
                case null:
                    throw new NullStudentException();

                case { } when IsInvalid(student.UpdatedBy):
                    throw new InvalidStudentException(
                        parameterName: nameof(Student.UpdatedBy),
                        parameterValue: student.UpdatedBy);
            }
        }

        private static bool IsInvalid(Guid id) => id == Guid.Empty;
        private static bool IsInvalid(string text) => String.IsNullOrWhiteSpace(text);
        private static bool IsInvalid(DateTimeOffset date) => date == default;
    }
}
