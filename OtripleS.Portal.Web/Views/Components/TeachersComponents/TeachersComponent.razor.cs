// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Models.Views.Components.TeachersComponents;
using OtripleS.Portal.Web.Services.TeacherViews;
using OtripleS.Portal.Web.Views.Bases;

namespace OtripleS.Portal.Web.Views.Components.TeachersComponents
{
    public partial class TeachersComponent
    {
        [Inject]
        public ITeacherViewService TeacherViewService { get; set; }

        public TeacherComponentState State { get; set; }
        public List<TeacherView> TeacherViews { get; set; }
        public GridBase<TeacherView> Grid { get; set; }
        public string ErrorMessage { get; set; }
        public LabelBase ErrorLabel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                this.TeacherViews =
                    await this.TeacherViewService.RetrieveAllTeacherViewsAsync();

                this.State = TeacherComponentState.Content;
            }
            catch(Exception exception)
            {
                this.ErrorMessage = exception.Message;
                this.State = TeacherComponentState.Error;
            }
        }
    }
}
