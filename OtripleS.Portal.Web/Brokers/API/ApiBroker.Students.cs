using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Students;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial class ApiBroker
    {
        private const string RelativeUrl = "api/students";

        public async ValueTask<Student> PostStudentAsync(Student student) =>
            await this.PostAsync(RelativeUrl, student);
    }
}
