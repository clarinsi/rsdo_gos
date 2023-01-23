using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;
using Gos.Services.Framework.Fragments;
using Gos.Tests.Unit.Services.Framework.Comparers;
using Xunit;

namespace Gos.Tests.Unit.Services.Framework.CorpusParser
{
    public class IncidentFragmentParserTests
    {
        [Fact]
        public void Sound()
        {
            // Arrange
            var xml = @"<incident xmlns=""http://www.tei-c.org/ns/1.0"" type=""sound""/>";
            var vocalEl = XElement.Parse(xml);
            var parser = new IncidentFragmentParser();

            // Act
            var tokens = parser.GetTokens(vocalEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[zvok]").WithStandardForm("[zvok]").WithLemma("[zvok]").WithMsd("mte:Xt"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}