using System.Globalization;

namespace Gos.Core.Entities
{
    public class PartOfSpeechAttributeValue : BaseEntity, ILocalizableEntity<PartOfSpeechAttributeValueTranslation>
    {
        public string Code { get; set; }

        public short RecordOrder { get; set; }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<PartOfSpeechAttributeValueTranslation> Translations { get; set; } = new();
    }
}