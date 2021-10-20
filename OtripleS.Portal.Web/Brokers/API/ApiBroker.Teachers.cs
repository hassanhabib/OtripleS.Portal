using System.Threading.Tasks;
using System.Collections.Generic;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string TechersRelativeUrl = "api/teachers";

        public async ValueTask<List<Teacher>> GetAllTechers() =>
            await this.GetAsync<List<Teacher>>(TechersRelativeUrl);

    }
}
