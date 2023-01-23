using System.Collections.Generic;
using System.Globalization;

namespace Gos.Core.Entities
{
    public class PartOfSpeechAttribute : BaseEntity, ILocalizableEntity<PartOfSpeechAttributeTranslation>
    {
        public short RecordOrder { get; set; }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<PartOfSpeechAttributeTranslation> Translations { get; set; } = new();

        public List<PartOfSpeechAttributeValue> Values { get; set; } = new();
    }
}