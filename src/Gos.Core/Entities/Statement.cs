namespace Gos.Core.Entities
{
    public class Statement : BaseEntity
    {
        public string Code { get; set; }

        public Discourse Discourse { get; set; }

        public int Order { get; set; }

        public Speaker Speaker { get; set; }
    }
}