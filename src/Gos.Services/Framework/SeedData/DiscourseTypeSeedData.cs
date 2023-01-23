using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class DiscourseTypeSeedData
    {
        public static IEnumerable<DiscourseType> Get()
        {
            yield return new DiscourseType
            {
                Id = (int)DiscourseTypeKeys.JavniInformativnoIzobrazevalni,
                Translations = new TranslationCollection<DiscourseTypeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "JI",
                        Title = "Javni informativno-izobraževalni",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "PI",
                        Title = "Public – informative and educational",
                    },
                },
            };

            yield return new DiscourseType
            {
                Id = (int)DiscourseTypeKeys.JavniRazvedrilni,
                Translations = new TranslationCollection<DiscourseTypeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "JR",
                        Title = "Javni razvedrilni",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "PE",
                        Title = "Public – entertainment",
                    },
                },
            };
            yield return new DiscourseType
            {
                Id = (int)DiscourseTypeKeys.NejavniNezasebni,
                Translations = new TranslationCollection<DiscourseTypeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "NN",
                        Title = "Nejavni nezasebni",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "NN",
                        Title = "Non-public – non-private",
                    },
                },
            };
            yield return new DiscourseType
            {
                Id = (int)DiscourseTypeKeys.NejavniZasebni,
                Translations = new TranslationCollection<DiscourseTypeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        ShortTitle = "NZ",
                        Title = "Nejavni zasebni",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        ShortTitle = "NP",
                        Title = "Non-public – private",
                    },
                },
            };
        }
    }
}