using System.Threading.Tasks;
using System.Collections.Generic;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string TeachersRelativeUrl = "api/teachers";

        public async ValueTask<List<Teacher>> GetAllTeachers() =>
            await this.GetAsync<List<Teacher>>(TeachersRelativeUrl);

    }
}
