using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Gos.Web.SelectLists;

public class WordSelectListProvider : ISelectListProvider
{
    private readonly IStringLocalizer<ConcordanceResource> concordanceResource;

    public WordSelectListProvider(IStringLocalizer<ConcordanceResource> concordanceResource)
    {
        this.concordanceResource = concordanceResource;
    }

    public Task<SelectList> Get(object selectedValue)
    {
        var items = new List<SelectListItem>
        {
            new()
            {
                Value = "[ime]",
                Text = concordanceResource["WordName"].Value,
            },
            new()
            {
                Value = "[priimek]",
                Text = concordanceResource["WordSurname"].Value,
            },
            new()
            {
                Value = "[nerazumljivo]",
                Text = concordanceResource["WordUnclear"].Value,
            },
            new()
            {
                Value = "[premor]",
                Text = concordanceResource["WordPause"].Value,
            },
        };

        var selectList = new SelectList(items, nameof(SelectListItem.Value), nameof(SelectListItem.Text), selectedValue);
        return Task.FromResult(selectList);
    }
}