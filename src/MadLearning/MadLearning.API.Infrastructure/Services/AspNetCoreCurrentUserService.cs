using MadLearning.API.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace MadLearning.API.Infrastructure.Services
{
    internal sealed class AspNetCoreCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AspNetCoreCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserInfo GetUserInfo()
        {
            var httpContext = this.httpContextAccessor.HttpContext;

            if (httpContext is null)
                throw new Exception("Can't get current user, is not in ASP.NET Core context");

            if (httpContext.User == null || httpContext.User.Identity?.Name == null)
            {
                throw new Exception("Can't get current user, is not authenticated");
            }

            var user = httpContext.User;

            var email = user.Identity.Name;
            var firstName = user.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value;
            var lastName = user.Claims.Single(c => c.Type == ClaimTypes.Surname).Value;
            var id = user.Claims.Single(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            return new UserInfo(id, email, firstName, lastName);
        }
    }
}
