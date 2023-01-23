using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Gos.Services.Services.PartOfSpeechService
{
    public class PartOfSpeechService : IPartOfSpeechService
    {
        private readonly GosDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public PartOfSpeechService(GosDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }

        public async Task<List<string>> BuildMsds(int partOfSpeechId, string attributes)
        {
            // Get part of speech
            var partOfSpeech = await dbContext.PartOfSpeeches.Include(p => p.Attributes)
                .ThenInclude(a => a.Values)
                .SingleOrDefaultAsync(p => p.Id == partOfSpeechId);
            if (partOfSpeech == null)
            {
                return null;
            }

            var attrValues = new Dictionary<int, HashSet<string>>();
            foreach (var attr in attributes.Split(","))
            {
                var attrData = attr.Split("_");
                var order = int.Parse(attrData[0]);
                if (!attrValues.ContainsKey(order))
                {
                    attrValues.Add(order, new HashSet<string>());
                }

                attrValues[order].Add(attrData[1]);
            }

            var results = new List<string>();
            var msds = await GetMsds();
            foreach (var msd in msds)
            {
                var msdCode = msd.Code;
                if (!msdCode.StartsWith(partOfSpeech.Code))
                {
                    continue;
                }

                var matches = true;
                for (var i = 0; i < msdCode.Length; i++)
                {
                    if (msdCode[i] == '-')
                    {
                        continue;
                    }

                    if (attrValues.ContainsKey(i) && !attrValues[i].Contains(msdCode[i].ToString()))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    results.Add(msdCode);
                }
            }

            return results;
        }

        public async Task<List<Msd>> GetMsds()
        {
            var cacheKey = Cache.CacheKeys.Msd.All();
            var msds = memoryCache.Get<List<Msd>>(cacheKey);
            if (msds == null)
            {
                msds = await dbContext.Msds.Include(m => m.Translations).ToListAsync();
                memoryCache.Set(cacheKey, msds, Cache.Duration.Long);
            }

            return msds;
        }

        public async Task<string> GetMsdDescriptionByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            var cacheKey = Cache.CacheKeys.Msd.ByCode(code);
            var msd = memoryCache.Get<Msd>(cacheKey);
            if (msd == null)
            {
                msd = await dbContext.Msds.Include(m => m.Translations).SingleOrDefaultAsync(m => m.Code == code);
                memoryCache.Set(cacheKey, msd, Cache.Duration.Long);
            }

            return msd?.Title;
        }

        public async Task<PartOfSpeech> GetPartOfSpeechByMsdCode(string code)
        {
            var partOfSpeechCode = GetPartOfSpeechCodeFromCode(code);
            if (string.IsNullOrEmpty(partOfSpeechCode))
            {
                return null;
            }

            var cacheKey = Cache.CacheKeys.PartOfSpeech.ByCode(partOfSpeechCode);
            var partOfSpeech = memoryCache.Get<PartOfSpeech>(cacheKey);
            if (partOfSpeech == null)
            {
                partOfSpeech = await dbContext.PartOfSpeeches.Include(p => p.Translations).SingleOrDefaultAsync(p => p.Code == partOfSpeechCode);
                memoryCache.Set(cacheKey, partOfSpeech, Cache.Duration.Long);
            }

            return partOfSpeech;
        }

        public static string GetPartOfSpeechCodeFromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }

            return code[..1];
        }
    }
}