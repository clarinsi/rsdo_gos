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
    public class CharacterFragmentParserTests
    {
        [Fact]
        public void Character()
        {
            // Arrange
            var xml = @"<pc xmlns=""http://www.tei-c.org/ns/1.0"" ana=""mte:Z"" msd=""UPosTag=PUNCT"" xml:id=""GosVL03_medit.tok349"">?</pc>";
            var characterEl = XElement.Parse(xml);
            var parser = new CharacterFragmentParser();

            // Act
            var tokens = parser.GetTokens(characterEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Character).WithConversationForm("?").WithStandardForm("?").WithLemma("?").WithMsd("mte:Z")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}