// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OtripleS.Portal.Web.Views.Bases
{
    public partial class GridBase<T> : ComponentBase
    {
        [Parameter]
        public List<T> DataSource { get; set; }
    }
}
