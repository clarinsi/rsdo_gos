using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Search;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;
using Gos.ServiceModel.Types;
using Gos.Services.Search.Aggregations;
using Gos.Services.Search.AlternateSearches;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.StatementService;
using MediatR;

namespace Gos.Services.RequestHandlers.Concordance
{
    public class ConcordanceSearchHandler : BaseConcordanceHandler, IRequestHandler<ConcordanceSearch, ConcordanceSearchResponse>
    {
        private readonly IAggregationProviderFactory aggregationProviderFactory;
        private readonly IAlternateSearchProviderFactory alternateSearchProviderFactory;
        private readonly IQueryFactory<ConcordanceSearch, ConcordanceQuery> queryFactory;
        private readonly ISearchEngine searchEngine;

        public ConcordanceSearchHandler(
            IAggregationProviderFactory aggregationProviderFactory,
            IAlternateSearchProviderFactory alternateSearchProviderFactory,
            IQueryFactory<ConcordanceSearch, ConcordanceQuery> queryFactory,
            ISearchEngine searchEngine,
            IStatementService statementService)
            : base(statementService)
        {
            this.aggregationProviderFactory = aggregationProviderFactory;
            this.alternateSearchProviderFactory = alternateSearchProviderFactory;
            this.queryFactory = queryFactory;
            this.searchEngine = searchEngine;
        }

        public async Task<ConcordanceSearchResponse> Handle(ConcordanceSearch request, CancellationToken cancellationToken)
        {
            // If there is no main word, just return empty result
            if (request.MainWord == null)
            {
                return new ConcordanceSearchResponse()
                {
                    Offset = request.From,
                    Total = 0,
                };
            }

            // Get query
            var query = await queryFactory.GetQuery(request);

            // Get results
            var results = searchEngine.Search<ConcordanceQuery, ConcordanceQueryResult>(query);

            // Return response
            return new ConcordanceSearchResponse
            {
                Items = await GetResponseItems(query, results.Items, true),
                Aggregations = new List<Aggregation>()
                {
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
                LemmasAlternateSearch = await GetLemmaSearches(request),
                Offset = request.From,
                Total = results.Total,
            };

            Aggregation GetAggregation(AggregationType type, Action<ConcordanceQuery> modificationAction)
            {
                var clonedQuery = (ConcordanceQuery)query.Clone();
                modificationAction(clonedQuery);
                return aggregationProviderFactory.GetProvider(type).Get(clonedQuery);
            }
        }

        private async Task<AlternateSearch<ConcordanceSearch>> GetLemmaSearches(ConcordanceSearch request)
        {
            if (request.MainWord.TranscriptionType != TranscriptionType.Standard)
            {
                return null;
            }

            var provider = alternateSearchProviderFactory.GetProvider<ConcordanceSearch, ConcordanceSearchResponse>(AlternateSearchType.Lemmas);
            return await provider.Get(request);
        }
    }
}