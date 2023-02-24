using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.List;
using Gos.ServiceModel.Types;
using Gos.Services.Search.Aggregations;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.PartOfSpeechService;
using MediatR;

namespace Gos.Services.RequestHandlers.List
{
    public class ListSearchHandler : BaseListHandler, IRequestHandler<ListSearch, ListSearchResponse>
    {
        private readonly IAggregationProviderFactory aggregationProviderFactory;
        private readonly IQueryFactory<ListSearch, ListQuery> queryFactory;
        private readonly ISearchEngine searchEngine;

        public ListSearchHandler(
            IAggregationProviderFactory aggregationProviderFactory,
            IPartOfSpeechService partOfSpeechService,
            IQueryFactory<ListSearch, ListQuery> queryFactory,
            ISearchEngine searchEngine)
            : base(partOfSpeechService)
        {
            this.aggregationProviderFactory = aggregationProviderFactory;
            this.queryFactory = queryFactory;
            this.searchEngine = searchEngine;
        }

        public async Task<ListSearchResponse> Handle(ListSearch request, CancellationToken cancellationToken)
        {
            // Get query
            var query = await queryFactory.GetQuery(request);

            // Get results
            var results = searchEngine.Search<ListQuery, ListQueryResult>(query);

            // Sort and page results
            var items = (await GetResponseItems(results.Items)).AsQueryable()
                .OrderBy(request.SortField, request.SortDirection == SortDirection.Descending)
                .Skip(request.From)
                .Take(request.Size)
                .ToList();

            // Map results
            return new ListSearchResponse()
            {
                Items = items,
                Aggregations = new List<Aggregation>()
                {
                    GetAggregation(AggregationType.Lemma, c => c.Lemmas?.Clear()),
                    GetAggregation(AggregationType.PartOfSpeech, c => c.PartOfSpeechIds?.Clear()),
                    GetAggregation(AggregationType.DiscourseType, c => c.DiscourseTypeIds?.Clear()),
                    GetAggregation(AggregationType.DiscourseChannel, c => c.DiscourseChannelIds?.Clear()),
                    GetAggregation(AggregationType.DiscourseYear, c => c.DiscourseYears?.Clear()),
                    GetAggregation(AggregationType.DiscourseEvent, c => c.DiscourseEventIds?.Clear()),
                    GetAggregation(AggregationType.SpeakerSex, c => c.SpeakerSexIds?.Clear()),
                    GetAggregation(AggregationType.SpeakerAge, c => c.SpeakerAgeIds?.Clear()),
                    GetAggregation(AggregationType.SpeakerRegion, c => c.SpeakerRegionIds?.Clear()),
                    GetAggregation(AggregationType.SpeakerEducation, c => c.SpeakerEducationIds?.Clear()),
                    GetAggregation(AggregationType.SpeakerLanguage, c => c.SpeakerLanguageIds?.Clear()),
                },
                Offset = request.From,
                Total = results.Total,
            };

            Aggregation GetAggregation(AggregationType type, Action<ListQuery> modificationAction)
            {
                var clonedQuery = (ListQuery)query.Clone();
                modificationAction(clonedQuery);
                return aggregationProviderFactory.GetProvider(type).Get(clonedQuery);
            }
        }
    }
}