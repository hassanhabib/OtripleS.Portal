// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class DatePickerBase
    {
        [Parameter]
        public DateTimeOffset Value { get; set; }

        [Parameter]
        public EventCallback<DateTimeOffset> ValueChanged { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        public async Task SetValue(DateTimeOffset value)
        {
            this.Value = value;
            await ValueChanged.InvokeAsync(this.Value);
        }

        private Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = DateTimeOffset.Parse(changeEventArgs.Value.ToString());

            return ValueChanged.InvokeAsync(this.Value);
        }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
