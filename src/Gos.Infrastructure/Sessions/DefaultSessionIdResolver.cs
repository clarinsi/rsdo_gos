using System;
using Gos.Core.Interfaces;

namespace Gos.Infrastructure.Sessions
{
    public class DefaultSessionIdResolver : ISessionIdResolver
    {
        public string Resolve()
        {
            return Guid.NewGuid().ToString();
        }
    }
}