using System;
using System.Net;
using Gos.Services.Framework;
using Gos.Web.Models.System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Gos.Web.Controllers
{
    public class SystemController : BaseController
    {
        public SystemController(GosDbContext dbContext)
            : base(dbContext)
        {
        }

        public IActionResult About()
        {
            var viewModel = CreateViewModel<SystemAboutViewModel>();
            var viewName = $"About_{CurrentUiLanguage.ToLower()}";
            return View(viewName, viewModel);
        }

        [Route("/error/404")]
        public IActionResult Error404()
        {
            var viewModel = GetErrorViewModel();
            return View(viewModel);
        }

        [Route("/error/{code:int}", Order = 100)]
        public IActionResult Error(int code)
        {
            // Log only internal server errors
            if (code == (int)HttpStatusCode.InternalServerError)
            {
                var error = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
                if (error != null)
                {
                    Log.Error(error, $"Error {code}");
                }
            }

            var viewModel = GetErrorViewModel();
            return View(viewModel);
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(1),
                });

            return LocalRedirect(returnUrl);
        }

        private SystemErrorViewModel GetErrorViewModel()
        {
            var viewModel = CreateViewModel<SystemErrorViewModel>();
            viewModel.RequestId = HttpContext.TraceIdentifier;
            viewModel.StatusCode = Response.StatusCode;
            return viewModel;
        }
    }
}