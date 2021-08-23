// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.StudentViews.Exceptions;
using OtripleS.Portal.Web.Services.StudentViews;
using OtripleS.Portal.Web.Views.Components;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Views.StudentRegistrationComponents
{
    public partial class StudentRegistrationComponentTests : TestContext
    {
        private readonly Mock<IStudentViewService> studentViewServiceMock;
        private IRenderedComponent<StudentRegistrationComponent> renderedStudentRegistrationComponent;

        public StudentRegistrationComponentTests()
        {
            this.studentViewServiceMock = new Mock<IStudentViewService>();
            this.Services.AddScoped(services => this.studentViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static StudentView CreateRandomStudentView() =>
            CreateStudentFiller().Create();

        private static string GetRandomString() => new MnemonicString().GetValue();

        public static TheoryData StudentViewValidationExceptions()
        {
            string randomMessage = GetRandomString();
            string validationMesage = randomMessage;
            var innerValidationException = new Exception(validationMesage);

            return new TheoryData<Exception>
            {
                new StudentViewValidationException(innerValidationException),
                new StudentViewDependencyValidationException(innerValidationException)
            };
        }

        public static TheoryData StudentViewDependencyServiceExceptions()
        {
            var innerValidationException = new Exception();

            return new TheoryData<Exception>
            {
                new StudentViewDependencyException(innerValidationException),
                new StudentViewServiceException(innerValidationException)
            };
        }

        private static Filler<StudentView> CreateStudentFiller()
        {
            var filler = new Filler<StudentView>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
