using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gos.Core;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Framework;
using Gos.Web.Models.Concordance;
using Gos.Web.SelectLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace Gos.Web.Controllers
{
    public class ConcordanceController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;
        private readonly ISelectListProviderFactory selectListProviderFactory;

        public ConcordanceController(
            IConfiguration configuration,
            GosDbContext dbContext,
            IMediator mediator,
            ISelectListProviderFactory selectListProviderFactory)
            : base(dbContext)
        {
            this.configuration = configuration;
            this.mediator = mediator;
            this.selectListProviderFactory = selectListProviderFactory;
        }

        public async Task<IActionResult> Advanced()
        {
            var viewModel = CreateViewModel<ConcordanceAdvancedViewModel>();
            viewModel.ConditionsList = await selectListProviderFactory.Get(SelectListType.Condition).Get(null);
            viewModel.LeftMarkList = await selectListProviderFactory.Get(SelectListType.Mark).Get(null);
            viewModel.PartOfSpeechesList = await selectListProviderFactory.Get(SelectListType.PartOfSpeech).Get(0);
            viewModel.RightMarkList = await selectListProviderFactory.Get(SelectListType.Mark).Get(null);
            viewModel.WordList = await selectListProviderFactory.Get(SelectListType.Word).Get(null);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(ConcordanceDetails request)
        {
            var viewModel = new ConcordanceDetailsViewModel
            {
                Result = await mediator.Send(request),
            };
            return PartialView("_Details", viewModel);
        }

        public async Task<IActionResult> Export(ConcordanceExport request)
        {
            var result = await mediator.Send(request);
            return File(result.Stream, result.ContentType, result.FileName);
        }

        public async Task<IActionResult> History([FromBody] IEnumerable<ConcordanceSearch> searches)
        {
            var partOfSpeeches = await DbContext.PartOfSpeeches.Include(p => p.Translations).ToDictionaryAsync(x => x.Id, x => x);
            var model = (searches, partOfSpeeches);
            return PartialView("_History", model);
        }

        public IActionResult Index()
        {
            var viewModel = CreateViewModel<ConcordanceIndexViewModel>();
            viewModel.Request = new ConcordanceSearch();
            return View(viewModel);
        }

        public async Task<IActionResult> Search(ConcordanceSearch request)
        {
            var viewModel = CreateViewModel<ConcordanceResultsViewModel>();
            viewModel.Request = request;
            viewModel.Result = await mediator.Send(request);
            return View("Results", viewModel);
        }

        public Task<IActionResult> Sound(string name)
        {
            var soundsFolder = configuration[ConfigurationKey.SoundFilesFolder];
            var soundFile = Path.Combine(soundsFolder, name);
            if (System.IO.File.Exists(soundFile))
            {
                return Task.FromResult<IActionResult>(new PhysicalFileResult(soundFile, new MediaTypeHeaderValue("audio/mpeg")));
            }

            return Task.FromResult<IActionResult>(NotFound());
        }
    }
}