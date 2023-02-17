using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerAge : BaseEntity, ILocalizableEntity<SpeakerAgeTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerAgeTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerAgeKeys
    {
        Do10Let = 1,
        Od10Do18Let = 2,
        Od18Do34Let = 3,
        Od30Do59Let = 4,
        Nad60Let = 5,
        Neznano = 6,
    }
}