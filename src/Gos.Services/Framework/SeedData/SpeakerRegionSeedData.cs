using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class SpeakerRegionSeedData
    {
        public static IEnumerable<SpeakerRegion> Get()
        {
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Celjska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "CE",
                        Title = "Celjska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "CE",
                        Title = "Celje",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Novogoriska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "GO",
                        Title = "Novogoriška",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "GO",
                        Title = "Nova Gorica",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Krska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "KK",
                        Title = "Krška",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "KK",
                        Title = "Krško",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Koprska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "KP",
                        Title = "Koprska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "KP",
                        Title = "Koper",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Kranjska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "KR",
                        Title = "Kranjska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "KR",
                        Title = "Kranj",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Ljubljanska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "LJ",
                        Title = "Ljubljanska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "LJ",
                        Title = "Ljubljana",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Mariborska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "MB",
                        Title = "Mariborska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "MB",
                        Title = "Maribor",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Murskosoboska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "MS",
                        Title = "Murskosoboška",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "MS",
                        Title = "Murska Sobota",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Novomeska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "NM",
                        Title = "Novomeška",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "NM",
                        Title = "Novo mesto",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Postojnska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "PO",
                        Title = "Postojnska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "PO",
                        Title = "Postojna",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Slovenjgraska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "SG",
                        Title = "Slovenjgraška",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "SG",
                        Title = "Slovenj Gradec",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Avstrija,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Avstrija",
                        Title = "Avstrija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Austria",
                        Title = "Austria",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Italija,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Italija",
                        Title = "Italija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Italy",
                        Title = "Italy",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Madzarska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Madžarska",
                        Title = "Madžarska",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Hungary",
                        Title = "Hungary",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Tujina,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Tujina",
                        Title = "Tujina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Outside Slovenia",
                        Title = "Outside Slovenia",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Neznano,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Neznano",
                        Title = "Neznano",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Unknown",
                        Title = "Unknown",
                    },
                },
            };
        }
    }
}