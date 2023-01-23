using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Search.Aggregations
{
    public class SpeakerEducationAggregationProvider : BaseAggregationProvider
    {
        private readonly GosDbContext dbContext;

        public SpeakerEducationAggregationProvider(IAggregatorFactory aggregatorFactory, GosDbContext dbContext)
            : base(aggregatorFactory)
        {
            this.dbContext = dbContext;
        }

        public override AggregationType Type => AggregationType.SpeakerEducation;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            var ids = items.Select(b => int.Parse(b.Key)).ToList();
            var types = dbContext.SpeakerEducations.Include(e => e.Translations).Where(tt => ids.Contains(tt.Id)).ToList();

            return (from item in items
                let id = int.Parse(item.Key)
                join education in types on id equals education.Id
                orderby item.Value descending, education.Title
                select new AggregationItem
                {
                    Count = item.Value,
                    Key = education.Id,
                    Title = education.Title,
                }).ToList();
        }
    }
}