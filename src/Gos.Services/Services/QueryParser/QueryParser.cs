using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Services.Services.QueryParser
{
    public class QueryParser : IQueryParser
    {
        public (ConcordanceSearchMainWord mainWord, List<ConcordanceSearchWordInContext> wordsInContext) Parse(
            string query,
            TranscriptionType transcriptionType)
        {
            ConcordanceSearchMainWord mainWord = null;
            List<ConcordanceSearchWordInContext> wordsInContext = null;

            var buffer = new StringBuilder();
            var inPhrase = false;
            for (int i = 0; i < query.Length; i++)
            {
                var c = query[i];
                if (c != '"')
                {
                    buffer.Append(c);
                }

                if (c == '"' || i == query.Length - 1)
                {
                    if (buffer.Length > 0)
                    {
                        var tokens = Tokenize(buffer.ToString());
                        foreach (var token in tokens)
                        {
                            if (mainWord == null)
                            {
                                mainWord = new ConcordanceSearchMainWord()
                                {
                                    Form = token,
                                    FormSearchType = inPhrase ? FormSearchType.ExactForm : FormSearchType.AllForms,
                                    TranscriptionType = transcriptionType,
                                };
                            }
                            else
                            {
                                wordsInContext ??= new List<ConcordanceSearchWordInContext>();
                                var wordInContext = new ConcordanceSearchWordInContext()
                                {
                                    Condition = ConditionType.Is,
                                    Form = token,
                                    FormSearchType = inPhrase ? FormSearchType.ExactForm : FormSearchType.AllForms,
                                    DistanceType = DistanceType.Position,
                                    LeftPosition = 0,
                                    RightPosition = wordsInContext.Count + 1,
                                    TranscriptionType = transcriptionType,
                                };
                                wordsInContext.Add(wordInContext);
                            }
                        }

                        buffer.Clear();
                    }

                    inPhrase = !inPhrase;
                }
            }

            return (mainWord, wordsInContext);
        }

        private static IEnumerable<string> Tokenize(string query)
        {
            // Sanitize search query
            var sanitized = SanitizeQuery(query);

            if (string.IsNullOrEmpty(sanitized))
            {
                return Enumerable.Empty<string>();
            }

            // Split query by spaces
            return sanitized.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private static string SanitizeQuery(string query)
        {
            var sb = new StringBuilder();
            foreach (var c in query)
            {
                if (char.IsDigit(c) || char.IsLetter(c) || char.IsWhiteSpace(c) || c is '[' or ']')
                {
                    sb.Append(c);
                }
                else if (c is '?' or '!' or '…')
                {
                    sb.Append(' ');
                    sb.Append(c);
                    sb.Append(' ');
                }
            }

            return sb.ToString();
        }
    }
}