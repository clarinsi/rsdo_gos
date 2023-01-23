using System.Linq;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.Services.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gos.Web.SelectLists
{
    public class PartOfSpeechSelectListProvider : ISelectListProvider
    {
        private readonly GosDbContext dbContext;

        public PartOfSpeechSelectListProvider(GosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SelectList> Get(object selectedValue)
        {
            var partOfSpeeches = await dbContext.PartOfSpeeches.Include(p => p.Attributes)
                .Include(p => p.Translations)
                .OrderBy(p => p.RecordOrder)
                .ToListAsync();
            return new SelectList(partOfSpeeches, nameof(PartOfSpeech.Id), nameof(PartOfSpeech.Title), selectedValue);
        }
    }
}