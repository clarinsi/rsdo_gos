using Autofac.Features.Indexed;

namespace Gos.Web.SelectLists
{
    public class SelectListProviderFactory : ISelectListProviderFactory
    {
        private readonly IIndex<SelectListType, ISelectListProvider> providers;

        public SelectListProviderFactory(IIndex<SelectListType, ISelectListProvider> providers)
        {
            this.providers = providers;
        }

        public ISelectListProvider Get(SelectListType type)
        {
            return providers[type];
        }
    }
}