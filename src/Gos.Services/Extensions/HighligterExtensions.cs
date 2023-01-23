using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Services.Extensions
{
    public static class HighligterExtensions
    {
        public static List<ConcordanceToken> Highlight(this List<ConcordanceToken> tokens, ConcordanceQuery query, int statementOrder, int tokenOrder)
        {
            var centerIndex = tokens.FindIndex(t => t.StatementOrder == statementOrder && t.TokenOrder == tokenOrder);
            if (centerIndex != -1)
            {
                tokens.Highlight(query, centerIndex);
            }

            return tokens;
        }

        public static List<ConcordanceToken> Highlight(this List<ConcordanceToken> tokens, ConcordanceQuery query, int centerTokenIndex)
        {
            var wordsInContextIndexes = query.WordsInContext.IsNullOrEmpty()
                ? new HashSet<int>()
                : GetWordsInContextIndexes(tokens, query, centerTokenIndex).ToHashSet();
            for (var i = 0; i < tokens.Count; i++)
            {
                // Center word
                if (i == centerTokenIndex)
                {
                    tokens[i].IsCenterMatch = true;
                }

                // Words in context
                if (wordsInContextIndexes.Contains(i))
                {
                    tokens[i].IsWordInContextMatch = true;
                }
            }

            return tokens;
        }

        private static bool CheckCandidateToken(List<ConcordanceToken> tokens, ConcordanceQuery query, int centerTokenIndex, int candidateTokenIndex)
        {
            foreach (var wordInContext in query.WordsInContext)
            {
                // Try to find candidates only on positive criteria
                if (wordInContext.Condition != ConditionType.Is)
                {
                    continue;
                }

                // Check positions
                if (wordInContext.DistanceType == DistanceType.Position)
                {
                    if (centerTokenIndex - wordInContext.LeftPosition != candidateTokenIndex &&
                        centerTokenIndex + wordInContext.RightPosition != candidateTokenIndex)
                    {
                        continue;
                    }
                }
                else if (wordInContext.DistanceType == DistanceType.Distance)
                {
                    var leftIndex = Math.Max(0, centerTokenIndex - wordInContext.LeftPosition);
                    var rightIndex = Math.Min(tokens.Count, centerTokenIndex + wordInContext.RightPosition);
                    if (candidateTokenIndex < leftIndex && candidateTokenIndex > rightIndex)
                    {
                        continue;
                    }
                }

                // Check candidate
                var candidate = tokens[candidateTokenIndex];
                if (!wordInContext.ConversationalForm.IsNullOrEmpty())
                {
                    if (!candidate.ConversationalForm.Equals(wordInContext.ConversationalForm, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                if (!wordInContext.StandardForm.IsNullOrEmpty())
                {
                    if (!candidate.StandardForm.Equals(wordInContext.StandardForm, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                if (!wordInContext.Lemmas.IsNullOrEmpty())
                {
                    if (!wordInContext.Lemmas.Any(l => l.Equals(candidate.Lemma, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }
                }

                return true;
            }

            return false;
        }

        private static IEnumerable<int> GetWordsInContextIndexes(List<ConcordanceToken> tokens, ConcordanceQuery query, int centerTokenIndex)
        {
            // Check up to 10 tokens (in both directions) for possible matches
            var leftIndex = Math.Max(0, centerTokenIndex - 10);
            var rightIndex = Math.Min(tokens.Count, centerTokenIndex + 10);
            for (var i = leftIndex; i < rightIndex; i++)
            {
                if (i != centerTokenIndex && CheckCandidateToken(tokens, query, centerTokenIndex, i))
                {
                    yield return i;
                }
            }
        }
    }
}