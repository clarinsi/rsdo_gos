using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Core.Entities;

namespace Gos.Services.Services.PartOfSpeechService
{
    public interface IPartOfSpeechService
    {
        Task<List<string>> BuildMsds(int partOfSpeechId, string attributes);

        Task<string> GetMsdDescriptionByCode(string code);

        Task<PartOfSpeech> GetPartOfSpeechByMsdCode(string code);
    }
}