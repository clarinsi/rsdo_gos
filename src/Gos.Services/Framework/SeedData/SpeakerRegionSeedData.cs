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
                Id = (int)SpeakerRegionKeys.Savinjska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Savinjska regija",
                        Title = "Savinjska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Savinja region",
                        Title = "Savinja region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Goriska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Goriška regija",
                        Title = "Goriška regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Gorica region",
                        Title = "Gorica region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Posavska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Posavska regija",
                        Title = "Posavska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Posavska region",
                        Title = "Posavska region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.ObalnoKraska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Obalno-kraška regija",
                        Title = "Obalno-kraška regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Coastal Karst region",
                        Title = "Coastal Karst region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Gorenjska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Gorenjska regija",
                        Title = "Gorenjska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Gorenjska region",
                        Title = "Gorenjska region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Osrednjeslovenska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Osrednjeslovenska regija",
                        Title = "Osrednjeslovenska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Central Slovenia",
                        Title = "Central Slovenia",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Podravska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Podravska regija",
                        Title = "Podravska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Drava region",
                        Title = "Drava region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Pomurska,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Pomurska regija",
                        Title = "Pomurska regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Pomurje region",
                        Title = "Pomurje region",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.JugovzhodnaSlovenija,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Jugovzhodna Slovenija",
                        Title = "Jugovzhodna Slovenija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Southeast Slovenia",
                        Title = "Southeast Slovenia",
                    },
                },
            };
            yield return new SpeakerRegion
            {
                Id = (int)SpeakerRegionKeys.Koroška,
                Translations = new TranslationCollection<SpeakerRegionTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "Koroška regija",
                        Title = "Koroška regija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "Carinthia region",
                        Title = "Carinthia region",
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