using System.Collections.Generic;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceDetailsResponseStatement
    {
        public string DiscourseChannel { get; set; }

        public string DiscourseDescription { get; set; }

        public string DiscourseEvent { get; set; }

        public string DiscourseSource { get; set; }

        public string DiscourseType { get; set; }

        public int Id { get; set; }

        public int NumberOfSpeakers { get; set; }

        public string SpeakerAge { get; set; }

        public string SpeakerCode { get; set; }

        public string SpeakerEducation { get; set; }

        public string SpeakerLanguage { get; set; }

        public string SpeakerRegion { get; set; }

        public string SpeakerSex { get; set; }

        public List<string> SoundFiles { get; set; }

        public List<ConcordanceToken> Tokens { get; set; }
    }
}