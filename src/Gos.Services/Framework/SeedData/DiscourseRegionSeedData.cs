using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class DiscourseRegionSeedData
    {
        public static IEnumerable<DiscourseRegion> Get()
        {
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Celjska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Novogoriska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Krska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Koprska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Kranjska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Ljubljanska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Mariborska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Murskosoboska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Novomeska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Postojnska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Slovenjgraska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.CelotnaSlovenija,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Slo",
                        Title = "Celotna Slovenija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Slo",
                        Title = "Slovenia",
                    },
                },
            };
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.SeverovzhodnaSlovenija,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "SV Slo",
                        Title = "Severovzhodna Slovenija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "SV Slo",
                        Title = "Northeastern Slovenia",
                    },
                },
            };
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.JugozahodnaSlovenija,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "JZ Slo",
                        Title = "Jugozahodna Slovenija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "JZ Slo",
                        Title = "Southwestern Slovenia",
                    },
                },
            };
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Avstrija,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Italija,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Madzarska,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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
            yield return new DiscourseRegion
            {
                Id = (int)DiscourseRegionKeys.Neznano,
                Translations = new TranslationCollection<DiscourseRegionTranslation>()
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