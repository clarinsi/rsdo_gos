using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;

namespace Gos.Services.Framework.Fragments
{
    public interface IFragmentParser
    {
        IEnumerable<Token> GetTokens(XElement element);
    }
}