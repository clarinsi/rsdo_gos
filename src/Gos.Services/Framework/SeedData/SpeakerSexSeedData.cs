using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class SpeakerSexSeedData
    {
        public static IEnumerable<SpeakerSex> Get()
        {
            yield return new SpeakerSex
            {
                Id = (int)SpeakerSexKeys.Moski,
                Translations = new TranslationCollection<SpeakerSexTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Moški",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Male",
                    },
                },
            };
            yield return new SpeakerSex
            {
                Id = (int)SpeakerSexKeys.Zenski,
                Translations = new TranslationCollection<SpeakerSexTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Ženski",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Female",
                    },
                },
            };
            yield return new SpeakerSex
            {
                Id = (int)SpeakerSexKeys.Neznano,
                Translations = new TranslationCollection<SpeakerSexTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Neznano",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Unknown",
                    },
                },
            };
        }
    }
}