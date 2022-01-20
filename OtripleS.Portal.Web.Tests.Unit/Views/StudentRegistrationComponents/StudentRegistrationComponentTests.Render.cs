// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.Colors;
using OtripleS.Portal.Web.Models.ContainerComponents;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Views.Components;
using OtripleS.Portal.Web.Views.Components.StudentRegistrationComponents;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Views.StudentRegistrationComponents
{
    public partial class StudentRegistrationComponentTests
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            ComponentState expectedComponentState =
                ComponentState.Loading;

            // when
            var initialStudentRegistrationComponent = new StudentRegistrationComponent();

            // then
            initialStudentRegistrationComponent.State.Should().Be(expectedComponentState);
            initialStudentRegistrationComponent.Exception.Should().BeNull();
            initialStudentRegistrationComponent.StudentIdentityTextBox.Should().BeNull();
            initialStudentRegistrationComponent.StudentFirstNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.StudentMiddleNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.StudentLastNameTextBox.Should().BeNull();
            initialStudentRegistrationComponent.SubmitButton.Should().BeNull();
            initialStudentRegistrationComponent.StudentView.Should().BeNull();
            initialStudentRegistrationComponent.StudentGenderDropDown.Should().BeNull();
            initialStudentRegistrationComponent.DateOfBirthPicker.Should().BeNull();
            initialStudentRegistrationComponent.StatusLabel.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderComponent()
        {
            // given
            ComponentState expectedComponentState =
               ComponentState.Content;

            string expectedIdentityTextBoxPlaceholder = "Student Identity";
            string expectedFirstNameTextBoxPlaceholder = "First Name";
            string expectedMiddleNameTextBoxPlaceholder = "Middle Name";
            string expectedLastnameTextBoxPlaceholder = "Last Name";
            string expectedSubmitButtonLabel = "SUBMIT";

            // when
            this.renderedStudentRegistrationComponents =
                RenderComponent<StudentRegistrationComponent>();

            // then
            this.renderedStudentRegistrationComponents.Instance.StudentView
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.State
                .Should().Be(expectedComponentState);

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox.Placeholder
                .Should().Be(expectedIdentityTextBoxPlaceholder);

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox.Placeholder
                .Should().Be(expectedFirstNameTextBoxPlaceholder);

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox.Placeholder
                .Should().Be(expectedMiddleNameTextBoxPlaceholder);

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox.Placeholder
                .Should().Be(expectedLastnameTextBoxPlaceholder);

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown.Value.GetType()
                .Should().Be(typeof(StudentViewGender));

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.Label
                .Should().Be(expectedSubmitButtonLabel);

            this.renderedStudentRegistrationComponents.Instance.SubmitButton
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.IsDisabled
                .Should().BeFalse();

            this.renderedStudentRegistrationComponents.Instance.StatusLabel
                .Should().NotBeNull();

            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Value
                .Should().BeNull();

            this.renderedStudentRegistrationComponents.Instance.Exception.Should().BeNull();
            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldDisplaySubmittingStatusAndDisabledControlsBeforeStudentSubmissionCompletes()
        {
            // given
            StudentView someStudentView = CreateRandomStudentView();

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ReturnsAsync(
                        value: someStudentView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when
            this.renderedStudentRegistrationComponents =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.Click();

            // then
            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Value
                .Should().BeEquivalentTo("Submitting ... ");

            this.renderedStudentRegistrationComponents.Instance.StatusLabel.Color
                .Should().Be(Color.Black);

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker.IsDisabled
               .Should().BeTrue();

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.IsDisabled
               .Should().BeTrue();

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSubmitStudent()
        {
            // given
            StudentView randomStudentView = CreateRandomStudentView();
            StudentView inputStudentView = randomStudentView;
            StudentView expectedStudentView = inputStudentView;
            string expectedOnSubmitRoute = "/studentsubmitted";

            // when
            this.renderedStudentRegistrationComponents =
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponents.Instance.StudentIdentityTextBox
                .SetValue(inputStudentView.IdentityNumber);

            this.renderedStudentRegistrationComponents.Instance.StudentFirstNameTextBox
                .SetValue(inputStudentView.FirstName);

            this.renderedStudentRegistrationComponents.Instance.StudentMiddleNameTextBox
                .SetValue(inputStudentView.MiddleName);

            this.renderedStudentRegistrationComponents.Instance.StudentLastNameTextBox
                .SetValue(inputStudentView.LastName);

            this.renderedStudentRegistrationComponents.Instance.StudentGenderDropDown
                .SetValue(inputStudentView.Gender);

            this.renderedStudentRegistrationComponents.Instance.DateOfBirthPicker
                .SetValue(inputStudentView.BirthDate);

            this.renderedStudentRegistrationComponents.Instance.SubmitButton.Click();

            // then
            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(
                    this.renderedStudentRegistrationComponents.Instance.StudentView),
                        Times.Once);

            this.studentViewServiceMock.Verify(service =>
                service.NavigateTo(expectedOnSubmitRoute),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
