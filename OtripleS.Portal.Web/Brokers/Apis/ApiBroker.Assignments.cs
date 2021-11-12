// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Assignments;

namespace OtripleS.Portal.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string AssignmentsRelativeUrl = "api/assignments";

        public async ValueTask<List<Assignment>> GetAllAssignmentsAsync() =>
            await this.GetAsync<List<Assignment>>(AssignmentsRelativeUrl);
    }
}
