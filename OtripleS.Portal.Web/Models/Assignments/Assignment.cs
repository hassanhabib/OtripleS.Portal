// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Models.Assignments
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Content { get; set; }
        public AssignmentStatus Status { get; set; }
    }
}
