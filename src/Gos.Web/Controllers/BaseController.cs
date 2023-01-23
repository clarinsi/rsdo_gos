using System.Linq;
using System.Threading;
using Gos.Services.Framework;
using Gos.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string CurrentUiLanguage => Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

        protected readonly GosDbContext DbContext;

        protected BaseController(GosDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected TViewModel CreateViewModel<TViewModel>()
            where TViewModel : BaseViewModel, new()
        {
            var viewModel = new TViewModel();

            if (viewModel is IIndexViewModel indexViewModel)
            {
                var counters = DbContext.CorpusCounters.FirstOrDefault();
                indexViewModel.Discourses = counters?.Discourses ?? 0;
                indexViewModel.Statements = counters?.Statements ?? 0;
                indexViewModel.Words = counters?.Words ?? 0;
            }

            return viewModel;
        }
    }
}