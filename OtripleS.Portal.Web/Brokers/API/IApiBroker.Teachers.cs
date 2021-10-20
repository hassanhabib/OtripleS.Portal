using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Teachers;
namespace OtripleS.Portal.Web.Brokers.API
{
      public partial interface IApiBroker
      {
            ValueTask<List<Teacher>> GetAllTeachers();
      }
}
