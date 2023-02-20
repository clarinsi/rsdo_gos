using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class SpeakerLanguageSeedData
    {
        public static IEnumerable<SpeakerLanguage> Get()
        {
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Slovenscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Slovenščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Slovene",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Nemscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Nemščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "German",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Anglescina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Angleščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "English",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Francoscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Francoščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "French",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Madzarscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Madžarščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Hungarian",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Hrvascina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Hrvaščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Croatian",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Srbscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Srbščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Serbian",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Italijanscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Italijanščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Italian",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Ruscina,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Ruščina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Russian",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Juznoslovanski,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Južnoslovanski",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "South Slavic",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.DrugiSlovanski,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Drugi slovanski",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Other Slavic",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.DrugiRomanski,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Drugi romanski",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Other Romanic",
                    },
                },
            };
            yield return new SpeakerLanguage
            {
                Id = (int)SpeakerLanguageKeys.Neznano,
                Translations = new TranslationCollection<SpeakerLanguageTranslation>()
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