// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class TextBoxBase : ComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        public void SetValue(string value) =>
            this.Value = value;

        public void SetPlaceholder(string value) =>
            this.Placeholder = value;
    }
}
