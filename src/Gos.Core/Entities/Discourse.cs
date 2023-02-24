using System;
using System.Collections.Generic;

namespace Gos.Core.Entities
{
    public class Discourse : BaseEntity
    {
        public DiscourseChannel Channel { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public DiscourseEvent Event { get; set; }

        public int NumberOfSpeakers { get; set; }

        public string Source { get; set; }

        public List<Statement> Statements { get; set; }

        public DiscourseType Type { get; set; }
    }
}