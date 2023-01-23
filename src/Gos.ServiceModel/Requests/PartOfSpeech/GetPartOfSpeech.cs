using MediatR;

namespace Gos.ServiceModel.Requests.PartOfSpeech
{
    public class GetPartOfSpeech : IRequest<GetPartOfSpeechResponse>
    {
        public int Id { get; set; }
    }
}