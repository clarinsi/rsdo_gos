using System.Collections.Generic;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Services.Services.QueryParser
{
    public interface IQueryParser
    {
        (ConcordanceSearchMainWord mainWord, List<ConcordanceSearchWordInContext> wordsInContext) Parse(string query, TranscriptionType transcriptionType);
    }
}