using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Extensions;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Web.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.UrlHelpers
{
    public static class ConcordanceUrlHelper
    {
        public static string ConcordanceDetailsLink(this IUrlHelper urlHelper, ConcordanceSearch search, int statementId, int tokenOrder)
        {
            var (_, items) = urlHelper.GetParsedQuery();
            items.Add(new KeyValuePair<string, string>($"{nameof(ConcordanceDetails.StatementId)}", statementId.ToString()));
            items.Add(new KeyValuePair<string, string>($"{nameof(ConcordanceDetails.TokenOrder)}", tokenOrder.ToString()));
            var qb = new QueryBuilder(items);
            return urlHelper.Action("Details", "Concordance") + qb.ToQueryString();
        }

        public static string ConcordanceSearchLink(this IUrlHelper urlHelper, ConcordanceSearch search, bool includeScheme = false)
        {
            var qb = GetBuilder(search);
            var scheme = includeScheme ? urlHelper.ActionContext.HttpContext.Request.Scheme : null;
            return urlHelper.Action("Search", "Concordance", null, scheme) + qb.ToQueryString();
        }

        public static string ConcordanceSoundLinks(this IUrlHelper urlHelper, List<string> soundFiles)
        {
            var urls = soundFiles.Select(
                    file => urlHelper.Action(
                        "Sound",
                        "Concordance",
                        new
                        {
                            name = file,
                        }))
                .ToList();
            return string.Join(",", urls);
        }

        public static bool IsActiveSearch(this IUrlHelper urlHelper, ConcordanceSearch search)
        {
            var qb = GetBuilder(search);
            var parsedQuery = urlHelper.GetParsedQuery();

            foreach (var item in qb)
            {
                if (!parsedQuery.items.Any(
                        x => x.Key.Equals(item.Key, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(item.Value, StringComparison.Ordinal)))
                {
                    return false;
                }
            }

            return true;
        }

        private static void AddWordValues(QueryBuilder qb, string baseName, ConcordanceSearchWord word)
        {
            if (!string.IsNullOrEmpty(word.Form))
            {
                qb.Add($"{baseName}.{nameof(ConcordanceSearchWord.Form)}", word.Form);
                qb.Add($"{baseName}.{nameof(ConcordanceSearchWord.TranscriptionType)}", word.TranscriptionType.ToString());
                qb.Add($"{baseName}.{nameof(ConcordanceSearchWord.FormSearchType)}", word.FormSearchType.ToString());
            }

            qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWord.ConversationalFormOverride)}", word.ConversationalFormOverride);
            qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWord.StandardFormOverride)}", word.StandardFormOverride);
            qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWord.Lemma)}", word.Lemma);

            if (word.PartOfSpeechId.HasValue || !word.Msds.IsNullOrEmpty())
            {
                qb.Add($"{baseName}.{nameof(ConcordanceSearchWord.PartOfSpeechCondition)}", word.PartOfSpeechCondition.ToString());
                qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWord.PartOfSpeechId)}", word.PartOfSpeechId?.ToString());
                qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWord.Msds)}", word.Msds);
            }
        }

        private static QueryBuilder GetBuilder<TResponse>(BaseConcordanceSearch<TResponse> search)
        {
            var qb = new QueryBuilder();

            if (search.MainWord != null)
            {
                var baseName = nameof(ConcordanceSearch.MainWord);
                AddWordValues(qb, baseName, search.MainWord);
            }
            else
            {
                qb.AddWithNullCheck($"{nameof(ConcordanceSearch.Query)}", search.Query);
                qb.Add($"{nameof(ConcordanceSearch.TranscriptionType)}", search.TranscriptionType.ToString());
            }

            if (!search.WordsInContext.IsNullOrEmpty())
            {
                for (var i = 0; i < search.WordsInContext.Count; i++)
                {
                    var wordInContext = search.WordsInContext[i];
                    var baseName = $"{nameof(ConcordanceSearch.WordsInContext)}[{i}]";
                    AddWordValues(qb, baseName, wordInContext);

                    qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWordInContext.Condition)}", wordInContext.Condition.ToString());
                    qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWordInContext.DistanceType)}", wordInContext.DistanceType.ToString());
                    qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWordInContext.LeftPosition)}", wordInContext.LeftPosition.ToString());
                    qb.AddWithNullCheck($"{baseName}.{nameof(ConcordanceSearchWordInContext.RightPosition)}", wordInContext.RightPosition.ToString());
                }
            }

            return qb;
        }
    }
}