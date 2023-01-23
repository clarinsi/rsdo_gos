using System.Globalization;

namespace Gos.Core.Entities
{
    public class DiscourseType : BaseEntity, ILocalizableEntity<DiscourseTypeTranslation>
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

        public TranslationCollection<DiscourseTypeTranslation> Translations { get; set; } = new();
    }

    public enum DiscourseTypeKeys
    {
        JavniInformativnoIzobrazevalni = 1,
        JavniRazvedrilni = 2,
        NejavniNezasebni = 3,
        NejavniZasebni = 4,
    }
}