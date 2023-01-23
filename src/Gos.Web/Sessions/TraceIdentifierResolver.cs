using System;
using Gos.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gos.Web.Sessions
{
    public class TraceIdentifierResolver : ITraceIdentifierResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TraceIdentifierResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Resolve()
        {
            return httpContextAccessor.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();
        }
    }
}