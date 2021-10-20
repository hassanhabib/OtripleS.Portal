using System.Threading.Tasks;
using System.Collections.Generic;
using OtripleS.Portal.Web.Models.Teachers;
namespace OtripleS.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        public ValueTask<List<Teacher>> GetAllTechers();
    }
}
