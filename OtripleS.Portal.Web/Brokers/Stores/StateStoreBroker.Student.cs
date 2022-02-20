using OtripleS.Portal.Web.Models.Students;
using OtripleS.Portal.Web.Models.StudentViews;
using OtripleS.Portal.Web.Services.Foundations.Students;

using System;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Brokers.Stores
{
    public partial class StateStoreBroker 
    {    
        public void StudentAdded()
        {
            BroadCastStateChange();
        }
    }
}
