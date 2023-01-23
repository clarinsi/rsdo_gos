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
    public class WordFragmentParserTests
    {
        [Fact]
        public void ConversationalFormSameAsStandardForm()
        {
            // Arrange
            var xml = @"<w xmlns=""http://www.tei-c.org/ns/1.0"" lemma=""med""
            ana=""mte:Si""
            msd=""UPosTag=ADP|Case=Ins""
            xml:id=""GosVL01_pravo.tok44"">med</w>";
            var wordEl = XElement.Parse(xml);
            var parser = new WordFragmentParser();

            // Act
            var tokens = parser.GetTokens(wordEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("med").WithStandardForm("med").WithLemma("med").WithMsd("mte:Si")
            };

            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void ConversationalFormDifferentAsStandardForm()
        {
            // Arrange
            var xml = @"<w xmlns=""http://www.tei-c.org/ns/1.0"" norm=""nekoliko""
            lemma=""nekoliko""
            ana=""mte:Rgp""
            msd=""UPosTag=ADV|Degree=Pos""
            xml:id=""GosVL01_pravo.tok47"">nekolk</w>";
            var wordEl = XElement.Parse(xml);
            var parser = new WordFragmentParser();

            // Act
            var tokens = parser.GetTokens(wordEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("nekolk").WithStandardForm("nekoliko").WithLemma("nekoliko").WithMsd("mte:Rgp")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void OneWordInConversationalFormMultipleWordsInStandardForm()
        {
            // Arrange
            var xml = @"<w xmlns=""http://www.tei-c.org/ns/1.0"" xml:id=""GosVL07_kungf.tok252"">novte<w norm=""ne""
            lemma=""ne""
            ana=""mte:Q""
            msd=""UPosTag=PART|Polarity=Neg""
            xml:id=""GosVL07_kungf.tok252.n1""/>
                <w norm=""boste""
            lemma=""biti""
            ana=""mte:Va-f2p-n""
            msd=""UPosTag=AUX|Mood=Ind|Number=Plur|Person=2|Polarity=Pos|Tense=Fut|VerbForm=Fin""
            xml:id=""GosVL07_kungf.tok252.n2""/>
                </w>";
            var wordEl = XElement.Parse(xml);
            var parser = new WordFragmentParser();

            // Act
            var tokens = parser.GetTokens(wordEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("novte").WithStandardForm("ne").WithLemma("ne").WithMsd("mte:Q"),
                new Token(TokenType.Word).WithStandardForm("boste").WithLemma("biti").WithMsd("mte:Va-f2p-n")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void MultipleWordsInConversationalFormOneWordInStandardForm()
        {
            // Arrange
            var xml = @"<w xmlns=""http://www.tei-c.org/ns/1.0"" norm=""JJ""
            lemma=""JJ""
            ana=""mte:Npmsn""
            msd=""UPosTag=PROPN|Case=Nom|Gender=Masc|Number=Sing""
            xml:id=""GosVL07_kungf.tok926"">
                <w xml:id=""GosVL07_kungf.tok926.o0"">Džej</w>
                <w xml:id=""GosVL07_kungf.tok926.o1"">Džej</w>
                </w>";
            var wordEl = XElement.Parse(xml);
            var parser = new WordFragmentParser();

            // Act
            var tokens = parser.GetTokens(wordEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("Džej").WithStandardForm("JJ").WithLemma("JJ").WithMsd("mte:Npmsn"),
                new Token(TokenType.Word).WithConversationForm("Džej")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}