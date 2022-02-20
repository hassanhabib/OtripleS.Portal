using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;

using System;

namespace OtripleS.Portal.Web.Brokers.Stores
{
    public interface IStateStoreBroker
    {
        void AddStateChangeListeners(Action listener);
        void RemoveStateChangeListeners(Action listener);
        void BroadCastStateChange();
        void StudentAdded();
    }
}
