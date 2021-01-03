// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using OtripleS.Portal.Web.Views.Components;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Views.StudentRegistrationComponents
{
    public partial class StudentRegistrationComponentTests
    {
        [Fact]
        public void ShouldRenderInnerExceptionMessageIfValidationErrorOccured()
        {
            // given
            string randomMessage = GetRandomString();
            string validationMesage = randomMessage;
            string expectedErrorMessage = validationMesage;
            var innerValidationException = new Exception(validationMesage);

            var studentViewValidationException =
                new StudentViewValidationException(innerValidationException);

            this.studentViewServiceMock.Setup(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()))
                    .ThrowsAsync(studentViewValidationException);

            // when
            this.renderedStudentRegistrationComponent = 
                RenderComponent<StudentRegistrationComponent>();

            this.renderedStudentRegistrationComponent.Instance.SubmitButton.Click();

            // then
            this.renderedStudentRegistrationComponent.Instance.ErrorLabel.Value
                .Should().BeEquivalentTo(expectedErrorMessage);

            this.studentViewServiceMock.Verify(service =>
                service.AddStudentViewAsync(It.IsAny<StudentView>()),
                    Times.Once);

            this.studentViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
