// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Classrooms;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string ClassroomsRelativeUrl = "api/classrooms";

        public async ValueTask<List<Classroom>> GetAllClassroomsAsync() =>
            await this.GetAsync<List<Classroom>>(ClassroomsRelativeUrl);
    }
}
