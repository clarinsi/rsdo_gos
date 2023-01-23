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
    public class PauseFragmentParserTests
    {
        [Fact]
        public void Pause()
        {
            // Arrange
            var xml = @"<pause xmlns=""http://www.tei-c.org/ns/1.0"" />";
            var pauseEl = XElement.Parse(xml);
            var parser = new PauseFragmentParser();

            // Act
            var tokens = parser.GetTokens(pauseEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[pavza]").WithStandardForm("[pavza]").WithLemma("[pavza]").WithMsd("mte:Xt")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}