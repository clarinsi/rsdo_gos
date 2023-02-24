using System.Collections.Generic;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class DiscourseEventSeedData
    {
        public static IEnumerable<DiscourseEvent> Get()
        {
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.NovinarskiPrispevek,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Novinarski prispevek",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "News report",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ModeriraniPogovorTv,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Moderirani pogovor",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Moderated discussion",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ModeriranaOddaja,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Moderirana oddaja",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Moderated show",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ResnicnostniSov,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Resničnostni šov",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Reality show",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.SportniPrenos,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Športni prenos",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Sports broadcast",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ModeriraniProgram,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Moderirani program",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Moderated broadcast",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ModeriraniPogovorRadio,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Moderirani pogovor",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Moderated discussion",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.OsnovnosolskaUcnaUra,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Osnovnošolska učna ura",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Elementary school lesson",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.SrednjesolskaUcnaUra,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Srednješolska učna ura",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Secondary school lesson",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Tecaj,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Tečaj",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Course",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.FakultetnoPredavanje,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Fakultetno predavanje",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "University lecture",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.JavnoPredavanje,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Javno predavanje",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Public lecture",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.FormalniDelovniSestanek,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Formalni delovni sestanek",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Formal work meeting",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.NeformalniDelovniSestanek,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Neformalni delovni sestanek",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Informal work meeting",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Konzultacija,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Konzultacija",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Consultation",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Storitev,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Storitev",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Service",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.FormalniRazgovor,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Formalni razgovor",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Formal interview",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ProdajaTrgovina,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Prodaja/trgovina",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Sales/Trade",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Svetovanje,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Svetovanje",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Advising",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Informacije,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Informacije",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Information desk",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Tajnistvo,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Tajništvo",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Administration office",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.PogovorVDruzini,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Pogovor v družini",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Family conversation",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.PogovorMedPrijateljiZnanci,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Pogovor med prijatelji/znanci",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Conversation between friends",
                    },
                },
            };

            // GosVL in Artur
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Predavanje,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Predavanje",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Lecture",
                    },
                },
            };

            // Artur
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.SejaDrzavnegaZbora,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Seja državnega zbora",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Sessions of the National Assembly",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.OkroglaMiza,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Okrogla miza",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Round table",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Intervju,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Intervju",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Interview",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.NagovorNaDogodku,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Nagovor na dogodku",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Speech at the event",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.NovinarskaKonferenca,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Novinarska konferenca",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Press conference",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.SpletniDogodek,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Spletni dogodek",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Online event",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.Seminar,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Seminar",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Seminar",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ProstiDialogMedDvemaSogovornikoma,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Prosti dialog med dvema sogovornikoma",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Casual dialogue between two collocutors",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.ProstiMonoloskiGovor,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Prosti monološki govor",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Casual monologue speech",
                    },
                },
            };
            yield return new DiscourseEvent
            {
                Id = (int)DiscourseEventKeys.RazlaganjeInOpisovanje,
                Translations = new TranslationCollection<DiscourseEventTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = "Razlaganje in opisovanje",
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = "Explaining and describing",
                    },
                },
            };
        }
    }
}