// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using Bunit;
using FluentAssertions;
using Moq;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.Views.Components.TeachersComponents;
using OtripleS.Portal.Web.Views.Components.TeachersComponents;
using Xunit;

namespace OtripleS.Portal.Web.Tests.Unit.Views.TeachersComponents
{
    public partial class TeachersComponentTests : TestContext
    {
        [Fact]
        public void ShouldInitComponent()
        {
            // given
            TeacherComponentState expectedState =
                TeacherComponentState.Loading;

            // when
            var initialTeachersComponent =
                new TeachersComponent();

            // then
            initialTeachersComponent.TeacherViewService.Should().BeNull();
            initialTeachersComponent.State.Should().Be(expectedState);
            initialTeachersComponent.TeacherViews.Should().BeNull();
            initialTeachersComponent.Grid.Should().BeNull();
            initialTeachersComponent.ErrorMessage.Should().BeNull();
            initialTeachersComponent.ErrorLabel.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderTeachers()
        {
            // given
            TeacherComponentState expectedState =
                TeacherComponentState.Content;

            List<TeacherView> randomTeacherViews =
                CreateRandomTeacherViews();

            List<TeacherView> retrievedTeacherViews =
                randomTeacherViews;

            List<TeacherView> expectedTeacherViews =
                retrievedTeacherViews;

            this.teacherViewServiceMock.Setup(service =>
                service.RetrieveAllTeachersAsync())
                    .ReturnsAsync(retrievedTeacherViews);

            // when
            this.renderedTeachersComponent =
                RenderComponent<TeachersComponent>();

            // then
            this.renderedTeachersComponent.Instance.State.Should()
                .Be(expectedState);

            this.renderedTeachersComponent.Instance.TeacherViews.Should()
                .BeEquivalentTo(expectedTeacherViews);

            this.renderedTeachersComponent.Instance.Grid.Should()
                .NotBeNull();

            this.renderedTeachersComponent.Instance.Grid.DataSource.Should()
                .BeEquivalentTo(expectedTeacherViews);

            this.renderedTeachersComponent.Instance.ErrorMessage.Should()
                .BeNull();

            this.renderedTeachersComponent.Instance.ErrorLabel.Should()
                .BeNull();

            this.teacherViewServiceMock.Verify(service =>
                service.RetrieveAllTeachersAsync(),
                    Times.Once);

            this.teacherViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
