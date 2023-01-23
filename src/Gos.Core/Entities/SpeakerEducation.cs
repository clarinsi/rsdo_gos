using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerEducation : BaseEntity, ILocalizableEntity<SpeakerEducationTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerEducationTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerEducationKeys
    {
        OsAliManj = 1,
        SrednjaSola = 2,
        VisjaAliVisokaSola = 3,
        FakultetaAliVec = 4,
        Neznano = 5,
    }
}