using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.ServiceModel.Requests.List;
using Gos.Services.Framework;
using Gos.Web.Models.List;
using Gos.Web.SelectLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.Controllers
{
    public class ListController : BaseController
    {
        private readonly IMediator mediator;
        private readonly ISelectListProviderFactory selectListProviderFactory;

        public ListController(GosDbContext dbContext, IMediator mediator, ISelectListProviderFactory selectListProviderFactory)
            : base(dbContext)
        {
            this.mediator = mediator;
            this.selectListProviderFactory = selectListProviderFactory;
        }

        public async Task<IActionResult> Export(ListExport request)
        {
            var result = await mediator.Send(request);
            return File(result.Stream, result.ContentType, result.FileName);
        }

        public IActionResult History([FromBody] IEnumerable<ListSearch> searches)
        {
            return PartialView("_History", searches);
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = CreateViewModel<ListIndexViewModel>();
            viewModel.ConditionsList = await selectListProviderFactory.Get(SelectListType.Condition).Get(null);
            viewModel.PartOfSpeechesList = await selectListProviderFactory.Get(SelectListType.PartOfSpeech).Get(null);
            viewModel.Request = new ListSearch();
            return View(viewModel);
        }

        public async Task<IActionResult> Search(ListSearch request)
        {
            var viewModel = CreateViewModel<ListResultsViewModel>();
            viewModel.Result = await mediator.Send(request);
            viewModel.Request = request;
            viewModel.SortList = await selectListProviderFactory.Get(SelectListType.ListSort).Get(request);
            return View("Results", viewModel);
        }
    }
}