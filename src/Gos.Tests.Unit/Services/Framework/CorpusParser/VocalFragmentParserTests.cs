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
    public class VocalFragmentParserTests
    {
        [Fact]
        public void EmptyVocal()
        {
            // Arrange
            var xml = @"<vocal xmlns=""http://www.tei-c.org/ns/1.0"" type=""voice""/>";
            var vocalEl = XElement.Parse(xml);
            var parser = new VocalFragmentParser();

            // Act
            var tokens = parser.GetTokens(vocalEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[prazen]").WithStandardForm("[prazen]").WithLemma("[prazen]").WithMsd("mte:Xt")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void Vocal()
        {
            // Arrange
            var xml = @"<vocal xmlns=""http://www.tei-c.org/ns/1.0"" type=""pause"">
                <desc>eee</desc>
                </vocal>";
            var vocalEl = XElement.Parse(xml);
            var parser = new VocalFragmentParser();

            // Act
            var tokens = parser.GetTokens(vocalEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[eee]").WithStandardForm("[eee]").WithLemma("[eee]").WithMsd("mte:Xt")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}