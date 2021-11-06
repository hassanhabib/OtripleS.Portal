// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
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
    }
}
