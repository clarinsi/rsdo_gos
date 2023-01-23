namespace Gos.Web.SelectLists
{
    public interface ISelectListProviderFactory
    {
        ISelectListProvider Get(SelectListType type);
    }
}