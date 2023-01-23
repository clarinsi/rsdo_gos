namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceDetails : BaseConcordanceSearch<ConcordanceDetailsResponse>
    {
        public int StatementId { get; set; }

        public int TokenOrder { get; set; }
    }
}