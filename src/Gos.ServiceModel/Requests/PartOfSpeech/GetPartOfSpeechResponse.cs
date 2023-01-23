using System.Collections.Generic;

namespace Gos.ServiceModel.Requests.PartOfSpeech
{
    public class GetPartOfSpeechResponse
    {
        public List<GetPartOfSpeechResponseAttribute> Attributes = new();

        public string Code { get; set; }

        public string Title { get; set; }
    }
}