using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gos.Services.Services.LemmatizationService
{
    public interface ILemmatizationService
    {
        Task<List<string>> GetLemmas(string standardForm, bool enableWildCards);
    }
}