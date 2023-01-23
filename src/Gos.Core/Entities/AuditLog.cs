using System;

namespace Gos.Core.Entities
{
    public class AuditLog : BaseEntity
    {
        public long DurationMs { get; set; }

        public DateTime EndDate { get; set; }

        public string Message { get; set; }

        public string ObjectType { get; set; }

        public string SessionId { get; set; }

        public DateTime StartDate { get; set; }

        public string TraceIdentifier { get; set; }
    }
}