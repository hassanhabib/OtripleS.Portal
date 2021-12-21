// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class ValidationSummaryBase
    {
        [Parameter]
        public IDictionary ValidationData { get; set; }

        [Parameter]
        public string Key { get; set; }

        public IEnumerable<string> Errors
        {
            get => this.ValidationData?[Key] as IEnumerable<string>;
            set => Errors = value;
        }
    }
}
