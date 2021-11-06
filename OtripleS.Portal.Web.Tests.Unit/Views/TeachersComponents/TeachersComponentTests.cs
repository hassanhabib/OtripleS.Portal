// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Services.TeacherViews;
using OtripleS.Portal.Web.Views.Components.TeachersComponents;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;

namespace OtripleS.Portal.Web.Tests.Unit.Views.TeachersComponents
{
    public partial class TeachersComponentTests : TestContext
    {
        private readonly Mock<ITeacherViewService> teacherViewServiceMock;
        private IRenderedComponent<TeachersComponent> renderedTeachersComponent;

        public TeachersComponentTests()
        {
            this.teacherViewServiceMock = new Mock<ITeacherViewService>();
            this.Services.AddTransient(service => this.teacherViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static List<TeacherView> CreateRandomTeacherViews() =>
            CreateTeacherViewFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<TeacherView> CreateTeacherViewFiller() =>
            new Filler<TeacherView>();
    }
}
