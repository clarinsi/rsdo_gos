namespace Gos.Core.Entities
{
    public class Speaker : BaseEntity
    {
        public SpeakerAge Age { get; set; }

        public string Code { get; set; }

        public SpeakerEducation Education { get; set; }

        public SpeakerLanguage Language { get; set; }

        public SpeakerRegion Region1 { get; set; }

        public SpeakerRegion Region2 { get; set; }

        public SpeakerRegion Region3 { get; set; }

        public SpeakerSex Sex { get; set; }
    }
}