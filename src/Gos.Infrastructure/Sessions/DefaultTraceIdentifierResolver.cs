using System;
using Gos.Core.Interfaces;

namespace Gos.Infrastructure.Sessions
{
    public class DefaultTraceIdentifierResolver : ITraceIdentifierResolver
    {
        public string Resolve()
        {
            return Guid.NewGuid().ToString();
        }
    }
}