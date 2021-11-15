// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Teachers;

namespace OtripleS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string TeachersRelativeUrl = "api/teachers";

        public async ValueTask<List<Teacher>> GetAllTeachersAsync() =>
            await this.GetAsync<List<Teacher>>(TeachersRelativeUrl);
    }
}
