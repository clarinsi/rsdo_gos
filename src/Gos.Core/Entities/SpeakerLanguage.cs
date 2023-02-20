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
        Nemscina = 2,
        Anglescina = 3,
        Francoscina = 4,
        Madzarscina = 5,
        Hrvascina = 6,
        Srbscina = 7,
        Italijanscina = 8,
        Ruscina = 9,
        Juznoslovanski = 10,
        DrugiSlovanski = 11,
        DrugiRomanski = 12,
        Neznano = 13,
    }
}