using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerRegion : BaseEntity, ILocalizableEntity<SpeakerRegionTranslation>
    {
        public string ShortTitle
        {
            get => Translations[CultureInfo.CurrentCulture].ShortTitle;
            set => Translations[CultureInfo.CurrentCulture].ShortTitle = value;
        }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerRegionTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerRegionKeys
    {
        Celjska = 1,
        Novogoriska = 2,
        Krska = 3,
        Koprska = 4,
        Kranjska = 5,
        Ljubljanska = 6,
        Mariborska = 7,
        Murskosoboska = 8,
        Novomeska = 9,
        Postojnska = 10,
        Slovenjgraska = 11,
        Avstrija = 12,
        Italija = 13,
        Madzarska = 14,
        Tujina = 15,
        Neznano = 16,
    }
}