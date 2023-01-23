using System.Linq;
using Gos.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Gos.Services.Framework
{
    public class CacheWarmUp
    {
        private readonly GosDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public CacheWarmUp(GosDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }

        public void WarmUp()
        {
            InitializePartOfSpeeches();
            InitializeMsds();
        }

        private void InitializePartOfSpeeches()
        {
            // Get part of speeches
            var partOfSpeeches = dbContext.PartOfSpeeches.Include(p => p.Translations).ToList();

            // Cache individual msd by code
            foreach (var partOfSpeech in partOfSpeeches)
            {
                memoryCache.Set(Cache.CacheKeys.PartOfSpeech.ByCode(partOfSpeech.Code), partOfSpeech, Cache.Duration.Long);
            }
        }

        private void InitializeMsds()
        {
            // Get msds
            var msds = dbContext.Msds.Include(m => m.Translations).ToList();

            // Cache all msds
            memoryCache.Set(Cache.CacheKeys.Msd.All(), msds, Cache.Duration.Long);

            // Cache individual msd by code
            foreach (var msd in msds)
            {
                memoryCache.Set(Cache.CacheKeys.Msd.ByCode(msd.Code), msd, Cache.Duration.Long);
            }
        }
    }
}