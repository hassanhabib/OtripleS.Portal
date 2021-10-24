// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Portal.Web.Models.Teachers;
using OtripleS.Portal.Web.Models.TeacherViews;
using OtripleS.Portal.Web.Services.Teachers;

namespace OtripleS.Portal.Web.Services.TeacherViews
{
    public class TeacherViewService : ITeacherViewService
    {
        private readonly ITeacherService teacherService;

        public TeacherViewService(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        public async ValueTask<List<TeacherView>> RetrieveAllTeachers()
        {
            List<Teacher> teachers = 
                await teacherService.RetrieveAllTeachersAsync();

            return new List<TeacherView>();
        }
    }
}
