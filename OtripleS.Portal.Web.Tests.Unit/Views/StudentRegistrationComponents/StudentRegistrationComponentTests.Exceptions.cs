// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Colors;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Views.Components;
using OtripleS.Portal.Web.Views.Components.StudentRegistrationComponents;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Views.StudentRegistrationComponents
{
    public partial class StudentRegistrationComponentTests
    {
        [Theory]
        [MemberData(nameof(StudentViewValidationExceptions))]
        public void ShouldRenderInnerExceptionMessageIfValidationErrorOccured(
            Exception studentViewValidationException)
        {
            // given
            string expectedErrorMessage =
                studentViewValidationException.InnerException.Message;

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(studentViewValidationException);

            // when
            this.renderedStudentRegistrationComponents =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.Click();

            // then
            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Value
                .Should().BeEquivalentTo(expectedErrorMessage);

            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Color
                .Should().Be(Color.Red);

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.IsDisabled
               .Should().BeFalse();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(StudentViewDependencyServiceExceptions))]
        public void ShouldRenderOuterExceptionMessageIfDependencyOrServiceErrorOccured(
            Exception studentViewDependencyServiceException)
        {
            // given
            string expectedErrorMessage =
                studentViewDependencyServiceException.Message;

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(studentViewDependencyServiceException);

            // when
            this.renderedStudentRegistrationComponents =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.Click();

            // then
            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Value
                .Should().BeEquivalentTo(expectedErrorMessage);

            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Color
                .Should().Be(Color.Red);

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker.IsDisabled
               .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.IsDisabled
               .Should().BeFalse();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
