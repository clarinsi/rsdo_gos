using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class DiscourseChannelSeedData
    {
        public static IEnumerable<DiscourseChannel> Get()
        {
            yield return new DiscourseChannel
            {
                Id = (int)DiscourseChannelKeys.Radio,
                Translations = new TranslationCollection<DiscourseChannelTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Radio",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Radio",
                    },
                },
            };
            yield return new DiscourseChannel
            {
                Id = (int)DiscourseChannelKeys.Televizija,
                Translations = new TranslationCollection<DiscourseChannelTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Televizija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Television",
                    },
                },
            };
            yield return new DiscourseChannel
            {
                Id = (int)DiscourseChannelKeys.OsebniStik,
                Translations = new TranslationCollection<DiscourseChannelTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Osebni stik",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Personal contact",
                    },
                },
            };
            yield return new DiscourseChannel
            {
                Id = (int)DiscourseChannelKeys.Telefon,
                Translations = new TranslationCollection<DiscourseChannelTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Telefon",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Telephone",
                    },
                },
            };

            // Artur
            yield return new DiscourseChannel
            {
                Id = (int)DiscourseChannelKeys.Internet,
                Translations = new TranslationCollection<DiscourseChannelTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Internet",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Internet",
                    },
                },
            };
        }
    }
}