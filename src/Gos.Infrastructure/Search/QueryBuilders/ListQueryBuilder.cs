using System;
using System.Linq;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries.List;
using Gos.Infrastructure.Extensions;
using Gos.ServiceModel.Enums;
using OpenSearch.Client;
using ConditionType = Gos.ServiceModel.Enums.ConditionType;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public class ListQueryBuilder : BaseQueryBuilder, IQueryBuilder<ListQuery>
    {
        public QueryContainer Build(ListQuery query)
        {
            // Get filter queries
            var queries = GetFilterQueries(query);

            // Append main queries
            switch (query.TranscriptionType)
            {
                case TranscriptionType.Conversational:
                    queries.Add(
                        new WildcardQuery()
                        {
                            Field = "token.conversationalLower",
                            Value = query.Query.ToLower(),
                        });
                    break;
                case TranscriptionType.Standard:
                    if (query.Query.StartsWith("\"") && query.Query.EndsWith("\""))
                    {
                        var form = query.Query.Substring(1, query.Query.Length - 2);
                        queries.Add(
                            new WildcardQuery()
                            {
                                Field = "token.standardLower",
                                Value = form.ToLower(),
                            });
                    }
                    else
                    {
                        queries.Add(
                            new WildcardQuery()
                            {
                                Field = "token.lemmaLower",
                                Value = query.Query.ToLower(),
                            });
                    }

                    break;
                default:
                    throw new Exception($"Invalid TranscriptionType: {query.TranscriptionType.ToString()}!");
            }

            // Filter by conversational or standard form
            if (query.GroupByMsd)
            {
                if (!string.IsNullOrEmpty(query.ConversationalForm))
                {
                    queries.Add(
                        new TermQuery()
                        {
                            Field = "token.conversational",
                            Value = query.ConversationalForm,
                        });
                }

                if (!string.IsNullOrEmpty(query.StandardForm))
                {
                    queries.Add(
                        new TermQuery()
                        {
                            Field = "token.standard",
                            Value = query.StandardForm,
                        });
                }
            }

            // Filter by lemma
            if (!query.Lemmas.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "token.lemma",
                        Terms = query.Lemmas,
                    });
            }

            // Filter by part of speech
            if (!query.PartOfSpeechIds.IsNullOrEmpty() || !query.Msds.IsNullOrEmpty())
            {
                QueryContainer partOfSpeechQuery;
                if (!query.Msds.IsNullOrEmpty())
                {
                    partOfSpeechQuery = new TermsQuery()
                    {
                        Field = "token.msd",
                        Terms = query.Msds,
                    };
                }
                else
                {
                    partOfSpeechQuery = new TermsQuery()
                    {
                        Field = "token.partOfSpeechId",
                        Terms = query.PartOfSpeechIds.Cast<object>(),
                    };
                }

                if (query.Condition == ConditionType.IsNot)
                {
                    partOfSpeechQuery = !partOfSpeechQuery;
                }

                queries.Add(partOfSpeechQuery);
            }

            // Return query
            return queries.ToBooleanAndQuery();
        }
    }
}