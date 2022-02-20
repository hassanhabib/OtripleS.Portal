using Microsoft.AspNetCore.Components;

using OtripleS.Portal.Web.Brokers.Stores;
using OtripleS.Portal.Web.Models.ContainerComponents;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Models.Views.Components.StudentComponents;
using OtripleS.Portal.Web.Models.Views.Components.TeachersComponents;
using OtripleS.Portal.Web.Services.Views.StudentViews;

using System;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Views.Components.StudentComponents
{
    public partial class StudentOlderComponent : IDisposable
    {
        [Inject]
        public IStudentViewService StudentViewService { get; set; }

        [Inject]
        public IStateStoreBroker StateStoreBroker { get; set; }
        public StudentView StudentView { get; set; }
        public StudentComponentState State { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    StudentView = await StudentViewService.RetrieveOlderStudentdViewAsync();
                    StateStoreBroker.AddStateChangeListeners(HandleStudentAdded);
                    this.State = StudentComponentState.Content;
                }
                catch (Exception exception)
                {
                    this.ErrorMessage = exception.Message;
                    this.State = StudentComponentState.Error;
                }
                finally
                {
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        private async void HandleStudentAdded()
        {
            StudentView = await StudentViewService.RetrieveOlderStudentdViewAsync();
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            StateStoreBroker.RemoveStateChangeListeners(HandleStudentAdded);
        }
    }
}

