using System.Collections.Generic;

namespace Gos.ServiceModel.Requests.PartOfSpeech
{
    public class GetPartOfSpeechResponseAttribute
    {
        public short RecordOrder { get; set; }

        public string Title { get; set; }

        public List<GetPartOfSpeechResponseAttributeValue> Values { get; set; } = new();
    }
}