// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.Students.Exceptions;

namespace OtripleS.Portal.Web.Services.Foundations.Students
{
    public partial class StudentService
    {
        private static void ValidateStudent(Student student)
        {
            ValidateStudentIsNotNull(student);

            Validate(
                (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalid(student.IdentityNumber), Parameter: nameof(Student.IdentityNumber)),
                (Rule: IsInvalid(student.UserId), Parameter: nameof(Student.UserId)),
                (Rule: IsInvalid(student.FirstName), Parameter: nameof(Student.FirstName)),
                (Rule: IsInvalid(student.BirthDate), Parameter: nameof(Student.BirthDate)),
                (Rule: IsInvalid(student.CreatedDate), Parameter: nameof(Student.CreatedDate)),
                (Rule: IsInvalid(student.UpdatedDate), Parameter: nameof(Student.UpdatedDate)),
                (Rule: IsInvalid(student.CreatedBy), Parameter: nameof(Student.CreatedBy)),
                (Rule: IsInvalid(student.UpdatedBy), Parameter: nameof(Student.UpdatedBy)));
        }

        private static void ValidateStudentIsNotNull(Student student)
        {
            if (student is null)
            {
                throw new NullStudentException();
            }
        }

        private static dynamic IsInvalid(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset dateTimeOffset) => new
        {
            Condition = dateTimeOffset == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentException = new InvalidStudentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentException.ThrowIfContainsErrors();
        }
    }
}
