// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace OtripleS.Portal.Web.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        public Guid GetCurrentlyLoggedInUser() => Guid.NewGuid();
    }
}
