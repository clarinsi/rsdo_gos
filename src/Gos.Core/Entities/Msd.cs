using System.Globalization;

namespace Gos.Core.Entities
{
    public class Msd : BaseEntity, ILocalizableEntity<MsdTranslation>
    {
        public string Code { get; set; }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<MsdTranslation> Translations { get; set; } = new();
    }
}