using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gos.ServiceModel.Requests.PartOfSpeech;
using Gos.Services.Framework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.PartOfSpeech
{
    public class GetPartOfSpeechHandler : IRequestHandler<GetPartOfSpeech, GetPartOfSpeechResponse>
    {
        private readonly GosDbContext dbContext;

        public GetPartOfSpeechHandler(GosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetPartOfSpeechResponse> Handle(GetPartOfSpeech request, CancellationToken cancellationToken)
        {
            var partOfSpeech = await dbContext.PartOfSpeeches.Include(p => p.Translations)
                .Include(p => p.Attributes)
                .ThenInclude(a => a.Values)
                .ThenInclude(v => v.Translations)
                .Include(p => p.Attributes)
                .ThenInclude(a => a.Translations)
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (partOfSpeech == null)
            {
                return null;
            }

            return new GetPartOfSpeechResponse()
            {
                Code = partOfSpeech.Code,
                Title = partOfSpeech.Title,
                Attributes = partOfSpeech.Attributes.Select(
                        a => new GetPartOfSpeechResponseAttribute()
                        {
                            RecordOrder = a.RecordOrder,
                            Title = a.Title,
                            Values = a.Values.Select(
                                    v => new GetPartOfSpeechResponseAttributeValue()
                                    {
                                        Code = v.Code,
                                        RecordOrder = v.RecordOrder,
                                        Title = v.Title,
                                    })
                                .OrderBy(v => v.RecordOrder)
                                .ToList(),
                        })
                    .OrderBy(a => a.RecordOrder)
                    .ToList(),
            };
        }
    }
}