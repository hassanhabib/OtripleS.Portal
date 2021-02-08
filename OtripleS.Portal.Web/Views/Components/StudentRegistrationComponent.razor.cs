// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OtripleS.Portal.Web.Models.Colors;
using OtripleS.Portal.Web.Models.ContainerComponents;
using OtripleS.Portal.Web.Models.StudentRegistrationComponents.Exceptions;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using OtripleS.Portal.Web.Services.StudentViews;
using OtripleS.Portal.Web.Views.Bases;

namespace OtripleS.Portal.Web.Views.Components
{
    public partial class StudentRegistrationComponent
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
                ReportStudentSubmissionSucceeded();
            }
            catch (StudentViewValidationException studentViewValidationException)
            {
                string validationMessage = 
                    studentViewValidationException.InnerException.Message;

                ReportStudentSubmissionFailed(validationMessage);
            }
            catch (StudentViewDependencyValidationException dependencyValidationException)
            {
                string validationMessage =
                    dependencyValidationException.InnerException.Message;

                ReportStudentSubmissionFailed(validationMessage);
            }
            catch (StudentViewDependencyException studentViewDependencyException)
            {
                string validationMessage =
                    studentViewDependencyException.Message;

                ReportStudentSubmissionFailed(validationMessage);
            }
            catch (StudentViewServiceException studentViewServiceException)
            {
                string validationMessage =
                    studentViewServiceException.Message;

                ReportStudentSubmissionFailed(validationMessage);
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

        private void ReportStudentSubmissionSucceeded()
        {
            this.StatusLabel.SetColor(Color.Green);
            this.StatusLabel.SetValue("Submitted Successfully");
        }

        private void ReportStudentSubmissionFailed(string errorMessage)
        {
            this.StatusLabel.SetColor(Color.Red);
            this.StatusLabel.SetValue(errorMessage);
        }
    }
}
