using System;
using System.Threading;
using Gos.Core;

namespace Gos.Web.Models
{
    public abstract class BaseViewModel
    {
        public virtual string BodyCssClass { get; }

        public string CurrentUiLanguage => Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

        public string CurrentUiName => CurrentUiLanguage.Equals(Constants.InterfaceLanguages.English, StringComparison.OrdinalIgnoreCase)
            ? Constants.InterfaceLanguages.EnglishName
            : Constants.InterfaceLanguages.SloveneName;
    }
}