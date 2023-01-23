namespace Gos.Core.Entities
{
    public class Segment : BaseEntity
    {
        public int Order { get; set; }

        public string SoundFile { get; set; }

        public Statement Statement { get; set; }
    }
}