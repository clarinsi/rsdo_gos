using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerLanguage : BaseEntity, ILocalizableEntity<SpeakerLanguageTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerLanguageTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerLanguageKeys
    {
        Slovenscina = 1,
        Anglescina = 2,
        Nemscina = 3,
        Italijanscina = 4,
        Madzarscina = 5,
        Juznoslovanski = 6,
        Albanscina = 7,
        DrugiSlovanski = 8,
        DrugiGermanski = 9,
        DrugiRomanski = 10,
        Neindoevropski = 11,
        Neznano = 12,
    }
}