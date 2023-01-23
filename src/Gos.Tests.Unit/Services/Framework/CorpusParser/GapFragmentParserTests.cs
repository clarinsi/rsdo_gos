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
    public class GapFragmentParserTests
    {
        [Fact]
        public void Unclear()
        {
            // Arrange
            var xml = @"<gap xmlns=""http://www.tei-c.org/ns/1.0"" reason=""unclear"" />";
            var pauseEl = XElement.Parse(xml);
            var parser = new GapFragmentParser();

            // Act
            var tokens = parser.GetTokens(pauseEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[nerazumljivo]")
                    .WithStandardForm("[nerazumljivo]")
                    .WithLemma("[nerazumljivo]")
                    .WithMsd("mte:Cc"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void Anon()
        {
            // Arrange
            var xml = @"<gap xmlns=""http://www.tei-c.org/ns/1.0"" reason=""anon"" quantity=""1"" unit=""words"">
                <desc>[ime]</desc>
                </gap>";

            var pauseEl = XElement.Parse(xml);
            var parser = new GapFragmentParser();

            // Act
            var tokens = parser.GetTokens(pauseEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[ime]").WithStandardForm("[ime]").WithLemma("[ime]").WithMsd("mte:Xp"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}