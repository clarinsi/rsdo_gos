namespace Gos.ServiceModel.Types
{
    public class AlternateSearchItem<TRequest>
    {
        public int Count { get; set; }

        public TRequest Search { get; set; }

        public string Title { get; set; }
    }
}