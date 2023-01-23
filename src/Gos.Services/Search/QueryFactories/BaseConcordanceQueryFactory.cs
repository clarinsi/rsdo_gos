using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Services.LemmatizationService;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public abstract class BaseConcordanceQueryFactory
    {
        private readonly ILemmatizationService lemmatizationService;
        private readonly IPartOfSpeechService partOfSpeechService;

        protected BaseConcordanceQueryFactory(ILemmatizationService lemmatizationService, IPartOfSpeechService partOfSpeechService)
        {
            this.lemmatizationService = lemmatizationService;
            this.partOfSpeechService = partOfSpeechService;
        }

        protected async Task<ConcordanceQuery> GetQuery<TRequest, TResponse>(TRequest request)
            where TRequest : BaseConcordanceSearch<TResponse>
        {
            return new ConcordanceQuery
            {
                MainWord = await GetMainWordQuery(request.MainWord),
                WordsInContext = await GetWordsInContextQuery(request.WordsInContext),
            }.WithFilters(request);
        }

        private async Task<ConcordanceQueryMainWord> GetMainWordQuery(ConcordanceSearchMainWord word)
        {
            return await GetWordQuery<ConcordanceQueryMainWord>(word);
        }

        private async Task<ConcordanceQueryWordInContext> GetWordInContextQuery(ConcordanceSearchWordInContext wordInContext)
        {
            var wordQuery = await GetWordQuery<ConcordanceQueryWordInContext>(wordInContext);
            wordQuery.Condition = wordInContext.Condition;
            wordQuery.DistanceType = wordInContext.DistanceType;
            wordQuery.LeftPosition = wordInContext.LeftPosition;
            wordQuery.RightPosition = wordInContext.RightPosition;
            return wordQuery;
        }

        private async Task<T> GetWordQuery<T>(ConcordanceSearchWord word)
            where T : ConcordanceQueryWord, new()
        {
            var wordQuery = new T();

            if (!string.IsNullOrEmpty(word.ConversationalFormOverride) || !string.IsNullOrEmpty(word.StandardFormOverride))
            {
                wordQuery.ConversationalForm = word.ConversationalFormOverride;
                wordQuery.StandardForm = word.StandardFormOverride;
            }
            else
            {
                if (word.TranscriptionType == TranscriptionType.Conversational)
                {
                    wordQuery.ConversationalForm = word.Form;
                }
                else
                {
                    if (word.FormSearchType == FormSearchType.ExactForm)
                    {
                        wordQuery.StandardForm = word.Form;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(word.Lemma))
                        {
                            wordQuery.Lemmas = new List<string>()
                            {
                                word.Lemma,
                            };
                        }
                        else
                        {
                            wordQuery.Lemmas = await lemmatizationService.GetLemmas(word.Form, false);
                        }
                    }
                }
            }

            wordQuery.LeftMark = word.LeftMark;
            wordQuery.RightMark = word.RightMark;
            wordQuery.PartOfSpeechCondition = word.PartOfSpeechCondition;
            wordQuery.PartOfSpeechId = word.PartOfSpeechId;
            if (word.PartOfSpeechId.HasValue && !string.IsNullOrEmpty(word.Msds))
            {
                // Search by msds from attributes (advanced search)
                wordQuery.Msds = await partOfSpeechService.BuildMsds(word.PartOfSpeechId.Value, word.Msds);
            }
            else if (!string.IsNullOrEmpty(word.Msds))
            {
                // Search by exact msd (link from list results)
                wordQuery.Msds = new List<string>()
                {
                    word.Msds,
                };
            }

            return wordQuery;
        }

        private async Task<List<ConcordanceQueryWordInContext>> GetWordsInContextQuery(List<ConcordanceSearchWordInContext> wordsInContext)
        {
            if (wordsInContext.IsNullOrEmpty())
            {
                return null;
            }

            var wordQueries = new List<ConcordanceQueryWordInContext>();
            foreach (var wordInContext in wordsInContext)
            {
                var wordQuery = await GetWordInContextQuery(wordInContext);
                wordQueries.Add(wordQuery);
            }

            return wordQueries;
        }
    }
}