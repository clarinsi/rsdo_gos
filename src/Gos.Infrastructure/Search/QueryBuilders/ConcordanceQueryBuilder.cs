using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries.Concordance;
using Gos.Infrastructure.Extensions;
using Gos.ServiceModel.Enums;
using OpenSearch.Client;
using ConditionType = Gos.ServiceModel.Enums.ConditionType;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public class ConcordanceQueryBuilder : BaseQueryBuilder, IQueryBuilder<ConcordanceQuery>
    {
        public QueryContainer Build(ConcordanceQuery query)
        {
            // Get filter queries
            var queries = GetFilterQueries(query);

            // Main word queries
            queries.Add(GetMainWordQuery(query.MainWord));

            // Words in context queries
            if (!query.WordsInContext.IsNullOrEmpty())
            {
                queries.AddRange(query.WordsInContext.Select(GetWordInContextQuery));
            }

            // Merge queries
            var mergedQuery = queries.ToBooleanAndQuery();

            // Check if we should return only random rows (used in export)
            if (query.ReturnRandomRows)
            {
                mergedQuery = new FunctionScoreQuery()
                {
                    Query = mergedQuery,
                    Functions = new List<IScoreFunction>()
                    {
                        new RandomScoreFunction()
                        {
                            Seed = DateTime.Now.Ticks,
                        },
                    },
                };
            }

            return mergedQuery;
        }

        private QueryContainer GetMainWordQuery(ConcordanceQueryMainWord word)
        {
            return GetWordQuery(word, GetTokenField(0));
        }

        private QueryContainer GetWordInContextQuery(ConcordanceQueryWordInContext word)
        {
            // Get word positions
            var positions = GetPositions(word);

            var queries = new List<QueryContainer>();
            foreach (var position in positions)
            {
                var positionQuery = GetWordQuery(word, GetTokenField(position));
                queries.Add(positionQuery);
            }

            var query = queries.ToBooleanOrQuery();
            return word.Condition == ConditionType.Is ? query : !query;
        }

        private QueryContainer GetWordQuery(ConcordanceQueryWord word, string tokenField)
        {
            var queries = new List<QueryContainer>();

            if (!string.IsNullOrEmpty(word.ConversationalForm))
            {
                queries.Add(
                    new TermQuery
                    {
                        Field = $"{tokenField}.conversationalLower",
                        Value = word.ConversationalForm.ToLower(),
                    });
            }

            if (!string.IsNullOrEmpty(word.StandardForm))
            {
                queries.Add(
                    new TermQuery
                    {
                        Field = $"{tokenField}.standardLower",
                        Value = word.StandardForm.ToLower(),
                    });
            }

            if (!word.Lemmas.IsNullOrEmpty())
            {
                var lowercasedLemmas = word.Lemmas.Select(l => l.ToLower());
                queries.Add(
                    new TermsQuery()
                    {
                        Field = $"{tokenField}.lemmaLower",
                        Terms = lowercasedLemmas,
                    });
            }

            if (!string.IsNullOrEmpty(word.LeftMark))
            {
                queries.Add(
                    new TermQuery
                    {
                        Field = $"{tokenField}.leftMark",
                        Value = word.LeftMark,
                    });
            }

            if (!string.IsNullOrEmpty(word.RightMark))
            {
                queries.Add(
                    new TermQuery
                    {
                        Field = $"{tokenField}.rightMark",
                        Value = word.RightMark,
                    });
            }

            // If no criteria was specified, return MatchNoneQuery
            if (queries.Count == 0)
            {
                return new MatchNoneQuery();
            }

            if (word.PartOfSpeechId.HasValue || !word.Msds.IsNullOrEmpty())
            {
                QueryContainer partOfSpeechQuery;
                if (!word.Msds.IsNullOrEmpty())
                {
                    partOfSpeechQuery = new TermsQuery()
                    {
                        Field = $"{tokenField}.msd",
                        Terms = word.Msds,
                    };
                }
                else
                {
                    partOfSpeechQuery = new TermQuery()
                    {
                        Field = $"{tokenField}.partOfSpeechId",
                        Value = word.PartOfSpeechId.Value,
                    };
                }

                if (word.PartOfSpeechCondition == ConditionType.IsNot)
                {
                    partOfSpeechQuery = !partOfSpeechQuery;
                }

                queries.Add(partOfSpeechQuery);
            }

            return queries.ToBooleanAndQuery();
        }

        private static List<int> GetPositions(ConcordanceQueryWordInContext word)
        {
            var positions = new List<int>();
            AddPositions(word.LeftPosition, word.DistanceType, true);
            AddPositions(word.RightPosition, word.DistanceType, false);
            return positions;

            void AddPositions(int position, DistanceType distanceType, bool negative)
            {
                if (position != 0)
                {
                    if (distanceType == DistanceType.Position)
                    {
                        positions.Add(negative ? -position : position);
                    }
                    else
                    {
                        for (var i = 1; i <= position; i++)
                        {
                            positions.Add(negative ? -i : i);
                        }
                    }
                }
            }
        }

        private static string GetTokenField(int position)
        {
            return position switch
            {
                < 0 => $"tokenLeft{-position}",
                > 0 => $"tokenRight{position}",
                _ => "token",
            };
        }
    }
}