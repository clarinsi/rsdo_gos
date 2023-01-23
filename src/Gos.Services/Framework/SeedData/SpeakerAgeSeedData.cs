using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class SpeakerAgeSeedData
    {
        public static IEnumerable<SpeakerAge> Get()
        {
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Do10Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "do 10",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "below 10",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Od10Do14Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "10 do 14",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "10 to 14",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Od15Do18Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "15 do 18",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "15 to 18",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Od19Do24Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "19 do 24",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "19 to 24",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Od25Do34Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "25 do 34",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "25 to 34",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Od35Do59Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "35 do 59",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "35 to 59",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Nad60Let,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "nad 60",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "over 60",
                    },
                },
            };
            yield return new SpeakerAge
            {
                Id = (int)SpeakerAgeKeys.Neznano,
                Translations = new TranslationCollection<SpeakerAgeTranslation>()
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