// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using OtripleS.Portal.Web.Models.Courses;
using OtripleS.Portal.Web.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtripleS.Portal.Web.Brokers.API
{
    public partial interface IApiBroker
    {
        ValueTask<IEnumerable<Course>> GetAllCourseAsync();
    }
}
