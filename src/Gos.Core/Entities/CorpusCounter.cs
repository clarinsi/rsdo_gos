namespace Gos.Core.Entities
{
    public class CorpusCounter : BaseEntity
    {
        public int Discourses { get; set; }

        public int Statements { get; set; }

        public int Words { get; set; }
    }
}