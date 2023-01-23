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
        Od10Do14Let = 2,
        Od15Do18Let = 3,
        Od19Do24Let = 4,
        Od25Do34Let = 5,
        Od35Do59Let = 6,
        Nad60Let = 7,
        Neznano = 8,
    }
}