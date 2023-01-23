using System.Collections.Generic;
using System.Globalization;

namespace Gos.Core.Entities
{
    public class PartOfSpeech : BaseEntity, ILocalizableEntity<PartOfSpeechTranslation>
    {
        public List<PartOfSpeechAttribute> Attributes { get; set; } = new();

        public string Code { get; set; }

        public short RecordOrder { get; set; }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<PartOfSpeechTranslation> Translations { get; set; } = new();
    }
}