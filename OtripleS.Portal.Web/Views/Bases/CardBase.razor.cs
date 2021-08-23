// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class CardBase : ComponentBase
    {
        [Parameter]
		public string Title { get; set; }

		[Parameter]
		public string SubTitle { get; set; }

		[Parameter]
		public RenderFragment Content { get; set; }

        [Parameter]
		public RenderFragment Footer { get; set; }
	}
}
