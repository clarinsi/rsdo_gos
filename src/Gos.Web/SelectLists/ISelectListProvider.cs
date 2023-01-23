using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gos.Web.SelectLists
{
    public interface ISelectListProvider
    {
        Task<SelectList> Get(object selectedValue);
    }
}