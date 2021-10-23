﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using OtripleS.Portal.Web.Models.SemesterCourses;

namespace OtripleS.Portal.Web.Models.Classrooms
{
    public class Classroom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ClassroomStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public IEnumerable<SemesterCourse> SemesterCourses { get; set; }
    }
}
