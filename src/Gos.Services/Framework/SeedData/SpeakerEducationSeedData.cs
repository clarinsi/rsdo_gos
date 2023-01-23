using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class SpeakerEducationSeedData
    {
        public static IEnumerable<SpeakerEducation> Get()
        {
            yield return new SpeakerEducation
            {
                Id = (int)SpeakerEducationKeys.OsAliManj,
                Translations = new TranslationCollection<SpeakerEducationTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "OŠ ali manj",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Elementary or lower",
                    },
                },
            };
            yield return new SpeakerEducation
            {
                Id = (int)SpeakerEducationKeys.SrednjaSola,
                Translations = new TranslationCollection<SpeakerEducationTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Srednja šola",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Secondary",
                    },
                },
            };
            yield return new SpeakerEducation
            {
                Id = (int)SpeakerEducationKeys.VisjaAliVisokaSola,
                Translations = new TranslationCollection<SpeakerEducationTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Višja ali visoka šola",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Professional higher",
                    },
                },
            };
            yield return new SpeakerEducation
            {
                Id = (int)SpeakerEducationKeys.FakultetaAliVec,
                Translations = new TranslationCollection<SpeakerEducationTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Fakulteta ali več",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "University or more",
                    },
                },
            };
            yield return new SpeakerEducation
            {
                Id = (int)SpeakerEducationKeys.Neznano,
                Translations = new TranslationCollection<SpeakerEducationTranslation>()
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