// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class DatePickerBase
    {
        [Parameter]
        public DateTimeOffset Value { get; set; }

        public void SetValue(DateTimeOffset value) =>
            this.Value = value;
    }
}
