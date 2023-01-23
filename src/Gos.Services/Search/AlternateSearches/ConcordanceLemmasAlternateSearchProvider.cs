using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;
using Gos.ServiceModel.Types;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.LemmatizationService;

namespace Gos.Services.Search.AlternateSearches
{
    public class ConcordanceLemmasAlternateSearchProvider : IAlternateSearchProvider<ConcordanceSearch, ConcordanceSearchResponse>
    {
        private readonly ILemmatizationService lemmatizationService;
        private readonly IQueryFactory<ConcordanceSearch, ConcordanceQuery> queryFactory;
        private readonly ISearchEngine searchEngine;

        public AlternateSearchType Type => AlternateSearchType.Lemmas;

        public ConcordanceLemmasAlternateSearchProvider(
            ILemmatizationService lemmatizationService,
            IQueryFactory<ConcordanceSearch, ConcordanceQuery> queryFactory,
            ISearchEngine searchEngine)
        {
            this.lemmatizationService = lemmatizationService;
            this.queryFactory = queryFactory;
            this.searchEngine = searchEngine;
        }

        public async Task<AlternateSearch<ConcordanceSearch>> Get(ConcordanceSearch request)
        {
            // Main word
            var lemmas = new List<List<string>>
            {
                await GetLemmasForWord(request.MainWord),
            };

            // Words in context
            if (!request.WordsInContext.IsNullOrEmpty())
            {
                foreach (var wordInContext in request.WordsInContext)
                {
                    if (SkipWord(wordInContext))
                    {
                        continue;
                    }

                    lemmas.Add(await GetLemmasForWord(wordInContext));
                }
            }

            var products = lemmas.CartesianProduct();
            var alternateSearch = new AlternateSearch<ConcordanceSearch>()
            {
                Items = new List<AlternateSearchItem<ConcordanceSearch>>(),
                OriginalSearch = GetOriginalSearch(request),
                Type = AlternateSearchType.Lemmas,
            };
            foreach (var product in products)
            {
                var clone = request.Copy();

                // Main word
                clone.MainWord.Lemma = product.ElementAt(0);

                // Words
                if (!clone.WordsInContext.IsNullOrEmpty())
                {
                    for (var i = 0; i < clone.WordsInContext.Count; i++)
                    {
                        var wordInContext = clone.WordsInContext[i];
                        if (SkipWord(wordInContext))
                        {
                            continue;
                        }

                        clone.WordsInContext[i].Lemma = product.ElementAt(i + 1);
                    }
                }

                // Count
                var count = await GetCount(clone);
                if (count > 0)
                {
                    alternateSearch.Items.Add(
                        new AlternateSearchItem<ConcordanceSearch>()
                        {
                            Count = count,
                            Search = clone,
                            Title = GetTitle(clone),
                        });
                }
            }

            return alternateSearch;
        }

        private async Task<int> GetCount(ConcordanceSearch search)
        {
            var query = await queryFactory.GetQuery(search);
            var results = searchEngine.Search<ConcordanceQuery, ConcordanceQueryResult>(query);
            return (int)results.Total;
        }

        public async Task<List<string>> GetLemmasForWord(ConcordanceSearchWord word)
        {
            if (!string.IsNullOrEmpty(word.Form))
            {
                return await lemmatizationService.GetLemmas(word.Form, false);
            }

            return new List<string>();
        }

        private ConcordanceSearch GetOriginalSearch(ConcordanceSearch request)
        {
            var original = request.Copy();
            original.MainWord.Lemma = null;

            if (!original.WordsInContext.IsNullOrEmpty())
            {
                foreach (var wordInContext in original.WordsInContext)
                {
                    wordInContext.Lemma = null;
                }
            }

            return original;
        }

        private static string GetTitle(ConcordanceSearch search)
        {
            var sb = new StringBuilder();
            sb.Append(search.MainWord.Lemma);

            if (!search.WordsInContext.IsNullOrEmpty())
            {
                foreach (var wordInContext in search.WordsInContext)
                {
                    sb.Append(" ");
                    sb.Append(wordInContext.Lemma);
                }
            }

            return sb.ToString();
        }

        private static bool SkipWord(ConcordanceSearchWordInContext wordInContext)
        {
            return wordInContext.Condition == ConditionType.IsNot || string.IsNullOrEmpty(wordInContext.Form) && string.IsNullOrEmpty(wordInContext.Lemma);
        }
    }
}