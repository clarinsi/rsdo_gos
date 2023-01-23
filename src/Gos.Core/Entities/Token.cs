using Gos.Core.Model;

namespace Gos.Core.Entities
{
    public class Token : BaseEntity
    {
        public string ConversationalForm { get; set; }

        public int DiscourseOrder { get; set; }

        public string LeftMark { get; set; }

        public string Lemma { get; set; }

        public string Msd { get; set; }

        public int Order { get; set; }

        public string RightMark { get; set; }

        public Segment Segment { get; set; }

        public string StandardForm { get; set; }

        public TokenType Type { get; set; }

        public Token()
        {
        }

        public Token(TokenType type)
        {
            Type = type;
        }
    }
}