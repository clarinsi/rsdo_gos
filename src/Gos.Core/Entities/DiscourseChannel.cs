using System.Globalization;

namespace Gos.Core.Entities
{
    public class DiscourseChannel : BaseEntity, ILocalizableEntity<DiscourseChannelTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<DiscourseChannelTranslation> Translations { get; set; } = new();
    }

    public enum DiscourseChannelKeys
    {
        Radio = 1,
        Televizija = 2,
        OsebniStik = 3,
        Telefon = 4,
    }
}