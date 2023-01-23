using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.ServiceModel.Enums;
using Gos.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Gos.Web.SelectLists
{
    public class ConditionSelectListProvider : ISelectListProvider
    {
        private readonly IStringLocalizer<SharedResource> sharedResource;

        public ConditionSelectListProvider(IStringLocalizer<SharedResource> sharedResource)
        {
            this.sharedResource = sharedResource;
        }

        public Task<SelectList> Get(object selectedValue)
        {
            var items = new List<SelectListItem>()
            {
                new()
                {
                    Value = ConditionType.Is.ToString(),
                    Text = sharedResource["IsCondition"],
                },
                new()
                {
                    Value = ConditionType.IsNot.ToString(),
                    Text = sharedResource["IsNotCondition"],
                },
            };

            var selectList = new SelectList(items, nameof(SelectListItem.Value), nameof(SelectListItem.Text), selectedValue);
            return Task.FromResult(selectList);
        }
    }
}