namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceToken
    {
        public string ConversationalForm { get; set; }

        public bool IsCenterMatch { get; set; }

        public bool IsWordInContextMatch { get; set; }

        public string LeftMark { get; set; }

        public string Lemma { get; set; }

        public string Msd { get; set; }

        public string MsdDescription { get; set; }

        public string RightMark { get; set; }

        public int SegmentId { get; set; }

        public string StandardForm { get; set; }

        public int StatementId { get; set; }

        public int StatementOrder { get; set; }

        public int TokenOrder { get; set; }
    }
}