using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Gos.Web.SelectLists;

public class MarkSelectListProvider : ISelectListProvider
{
    private readonly IStringLocalizer<ConcordanceResource> concordanceResource;

    public MarkSelectListProvider(IStringLocalizer<ConcordanceResource> concordanceResource)
    {
        this.concordanceResource = concordanceResource;
    }

    public Task<SelectList> Get(object selectedValue)
    {
        var items = new List<SelectListItem>()
        {
            new()
            {
                Value = null,
                Text = concordanceResource["NoMark"].Value,
            },
            new()
            {
                Value = "[smeh]",
                Text = "[smeh]",
            },
            new()
            {
                Value = "[glas]",
                Text = "[glas]",
            },
            new()
            {
                Value = "[dih]",
                Text = "[dih]",
            },
            new()
            {
                Value = "[govor]",
                Text = "[govor]",
            },
        };

        var selectList = new SelectList(items, nameof(SelectListItem.Value), nameof(SelectListItem.Text), selectedValue);
        return Task.FromResult(selectList);
    }
}