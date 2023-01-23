using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerSex : BaseEntity, ILocalizableEntity<SpeakerSexTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerSexTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerSexKeys
    {
        Moski = 1,
        Zenski = 2,
        Neznano = 3,
    }
}