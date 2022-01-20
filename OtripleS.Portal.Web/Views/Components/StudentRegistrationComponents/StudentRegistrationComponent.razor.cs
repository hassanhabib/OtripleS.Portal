// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using OtripleS.Portal.Web.Models.Colors;
using OtripleS.Portal.Web.Models.ContainerComponents;
using OtripleS.Portal.Web.Models.StudentRegistrationComponents.Exceptions;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using OtripleS.Portal.Web.Services.Views.StudentViews;
using OtripleS.Portal.Web.Views.Bases;

namespace OtripleS.Portal.Web.Views.Components.StudentRegistrationComponents
{
    public partial class StudentRegistrationComponent : ComponentBase
    {
        [Inject]
        public IStudentViewService StudentViewService { get; set; }

        public ComponentState State { get; set; }
        public StudentRegistrationComponentException Exception { get; set; }
        public StudentView StudentView { get; set; }
        public TextBoxBase StudentIdentityTextBox { get; set; }
        public TextBoxBase StudentFirstNameTextBox { get; set; }
        public TextBoxBase StudentMiddleNameTextBox { get; set; }
        public TextBoxBase StudentLastNameTextBox { get; set; }
        public DropDownBase<StudentViewGender> StudentGenderDropDown { get; set; }
        public DatePickerBase DateOfBirthPicker { get; set; }
        public ButtonBase SubmitButton { get; set; }
        public LabelBase StatusLabel { get; set; }

        protected override void OnInitialized()
        {
            this.StudentView = new StudentView();
            this.State = ComponentState.Content;
        }

        public async void RegisterStudentAsync()
        {
            try
            {
                ApplySubmittingStatus();
                await this.StudentViewService.AddStudentViewAsync(this.StudentView);
                NavigateToStudentSubmittedPage();
            }
            catch (StudentViewValidationException studentViewValidationException)
            {
                string validationMessage =
                    studentViewValidationException.InnerException.Message;

                ApplySubmissionFailed(validationMessage);
            }
            catch (StudentViewDependencyValidationException dependencyValidationException)
            {
                string validationMessage =
                    dependencyValidationException.InnerException.Message;

                ApplySubmissionFailed(validationMessage);
            }
            catch (StudentViewDependencyException studentViewDependencyException)
            {
                string validationMessage =
                    studentViewDependencyException.Message;

                ApplySubmissionFailed(validationMessage);
            }
            catch (StudentViewServiceException studentViewServiceException)
            {
                string validationMessage =
                    studentViewServiceException.Message;

                ApplySubmissionFailed(validationMessage);
            }
        }

        private void ApplySubmittingStatus()
        {
            this.StatusLabel.SetColor(Color.Black);
            this.StatusLabel.SetValue("Submitting ... ");
            this.StudentIdentityTextBox.Disable();
            this.StudentFirstNameTextBox.Disable();
            this.StudentMiddleNameTextBox.Disable();
            this.StudentLastNameTextBox.Disable();
            this.StudentGenderDropDown.Disable();
            this.DateOfBirthPicker.Disable();
            this.SubmitButton.Disable();
        }

        private void NavigateToStudentSubmittedPage() =>
            this.StudentViewService.NavigateTo("/studentsubmitted");

        private void ApplySubmissionFailed(string errorMessage)
        {
            this.StatusLabel.SetColor(Color.Red);
            this.StatusLabel.SetValue(errorMessage);
            this.StudentIdentityTextBox.Enable();
            this.StudentFirstNameTextBox.Enable();
            this.StudentMiddleNameTextBox.Enable();
            this.StudentLastNameTextBox.Enable();
            this.StudentGenderDropDown.Enable();
            this.DateOfBirthPicker.Enable();
            this.SubmitButton.Enable();
        }
    }
}
