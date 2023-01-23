namespace Gos.ServiceModel.Interfaces
{
    public interface IPagedSearch
    {
        int From { get; }

        int Size { get; }
    }
}