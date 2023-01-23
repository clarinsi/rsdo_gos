using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Services.LemmatizationService
{
    public class LemmatizationService : ILemmatizationService
    {
        private readonly GosDbContext dbContext;

        public LemmatizationService(GosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<string>> GetLemmas(string standardForm, bool enableWildCards)
        {
            standardForm = standardForm.ToLower();
            if (enableWildCards)
            {
                var standardFormSql = standardForm.Replace("*", "%").Replace("?", "_");
                return await dbContext.CorpusForms.Where(f => EF.Functions.Like(f.StandardForm, standardFormSql)).Select(f => f.Lemma).Distinct().ToListAsync();
            }

            return await dbContext.CorpusForms.Where(f => f.StandardForm == standardForm).Select(f => f.Lemma).Distinct().ToListAsync();
        }
    }
}