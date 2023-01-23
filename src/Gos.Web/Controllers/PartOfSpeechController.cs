using System.Threading.Tasks;
using Gos.ServiceModel.Requests.PartOfSpeech;
using Gos.Services.Framework;
using Gos.Web.Models.PartOfSpeech;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.Controllers
{
    public class PartOfSpeechController : BaseController
    {
        private readonly IMediator mediator;

        public PartOfSpeechController(GosDbContext dbContext, IMediator mediator)
            : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Details(GetPartOfSpeech request)
        {
            var viewModel = new PartOfSpeechDetailsViewModel()
            {
                Result = await mediator.Send(request),
            };
            return PartialView("_Details", viewModel);
        }
    }
}