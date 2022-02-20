using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Foundations.Students;

using System;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Brokers.Stores
{
    public partial class StateStoreBroker : IStateStoreBroker
    {
        protected Action listeners;


        public void AddStateChangeListeners(Action listener)
            => this.listeners += listener;


        public void RemoveStateChangeListeners(Action listener)
            => this.listeners -= listener;


        public void BroadCastStateChange()
        {
            if (this.listeners != null) this.listeners.Invoke();
        }
    }
}
