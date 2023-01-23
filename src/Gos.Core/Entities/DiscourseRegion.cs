using System.Globalization;

namespace Gos.Core.Entities
{
    public class DiscourseRegion : BaseEntity, ILocalizableEntity<DiscourseRegionTranslation>
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

        public TranslationCollection<DiscourseRegionTranslation> Translations { get; set; } = new();
    }

    public enum DiscourseRegionKeys
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
        CelotnaSlovenija = 12,
        SeverovzhodnaSlovenija = 13,
        JugozahodnaSlovenija = 14,
        Avstrija = 15,
        Italija = 16,
        Madzarska = 17,
        Neznano = 18,
    }
}