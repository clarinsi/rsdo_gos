using System;
using System.Text;
using Gos.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gos.Web.Sessions
{
    public class SessionIdResolver : ISessionIdResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SessionIdResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Resolve()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var session = httpContext.Session;
                session.Set("Site", Encoding.UTF8.GetBytes("Gos"));
                return httpContext.Session.Id;
            }

            return Guid.NewGuid().ToString();
        }
    }
}