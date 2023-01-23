using System.Collections.Generic;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;
using Gos.Services.Framework.ConcordanceBuilder;
using Gos.Tests.Unit.Services.Framework.Comparers;
using Xunit;

namespace Gos.Tests.Unit.Services.Framework
{
    public class ConcordanceBuilderTests
    {
        [Fact]
        public void ReturnedConcordances()
        {
            // Arrange
            var token1 = new Token(TokenType.Word).WithConversationForm("to").WithStandardForm("to").WithLemma("ta").WithMsd("mte:Pd-nsn");
            var token2 = new Token(TokenType.Word).WithConversationForm("je").WithStandardForm("je").WithLemma("biti").WithMsd("mte:Va-r3s-n");
            var token3 = new Token(TokenType.Word).WithConversationForm("zdej").WithStandardForm("zdaj").WithLemma("zdaj").WithMsd("mte:Rgp");
            var token4 = new Token(TokenType.Word).WithConversationForm("to").WithStandardForm("to").WithLemma("ta").WithMsd("mte:Pd-nsa");
            var token5 = new Token(TokenType.Word).WithConversationForm("kar").WithStandardForm("kar").WithLemma("kar").WithMsd("mte:Pr-nsa");
            var token6 = new Token(TokenType.Word).WithConversationForm("bi").WithStandardForm("bi").WithLemma("biti").WithMsd("mte:Va-c");
            var token7 = new Token(TokenType.Word).WithConversationForm("zmerl").WithStandardForm("izmerili").WithLemma("izmeriti").WithMsd("mte:Vmep-pm");
            var token8 = new Token(TokenType.Word).WithConversationForm("na").WithStandardForm("na").WithLemma("na").WithMsd("mte:Sl");
            var token9 = new Token(TokenType.Word).WithConversationForm("somi").WithStandardForm("somi").WithLemma("som").WithMsd("mte:Ncmpn");
            var tokens = new List<Token>()
            {
                token1,
                token2,
                token3,
                token4,
                token5,
                token6,
                token7,
                token8,
                token9
            };
            var statement = new Statement();
            var builder = new ConcordanceBuilder();

            // Act
            var concordances = builder.GetConcordances(statement, tokens);

            // Assert
            var expected = new List<Concordance>()
            {
                new()
                {
                    Token = token1,
                    TokenRight1 = token2,
                    TokenRight2 = token3,
                    TokenRight3 = token4,
                    TokenRight4 = token5,
                    TokenRight5 = token6,
                    TokenRight6 = token7,
                    TokenRight7 = token8,
                    TokenRight8 = token9
                },
                new()
                {
                    TokenLeft1 = token1,
                    Token = token2,
                    TokenRight1 = token3,
                    TokenRight2 = token4,
                    TokenRight3 = token5,
                    TokenRight4 = token6,
                    TokenRight5 = token7,
                    TokenRight6 = token8,
                    TokenRight7 = token9
                },
                new()
                {
                    TokenLeft2 = token1,
                    TokenLeft1 = token2,
                    Token = token3,
                    TokenRight1 = token4,
                    TokenRight2 = token5,
                    TokenRight3 = token6,
                    TokenRight4 = token7,
                    TokenRight5 = token8,
                    TokenRight6 = token9
                },
                new()
                {
                    TokenLeft3 = token1,
                    TokenLeft2 = token2,
                    TokenLeft1 = token3,
                    Token = token4,
                    TokenRight1 = token5,
                    TokenRight2 = token6,
                    TokenRight3 = token7,
                    TokenRight4 = token8,
                    TokenRight5 = token9
                },
                new()
                {
                    TokenLeft4 = token1,
                    TokenLeft3 = token2,
                    TokenLeft2 = token3,
                    TokenLeft1 = token4,
                    Token = token5,
                    TokenRight1 = token6,
                    TokenRight2 = token7,
                    TokenRight3 = token8,
                    TokenRight4 = token9
                },
                new()
                {
                    TokenLeft5 = token1,
                    TokenLeft4 = token2,
                    TokenLeft3 = token3,
                    TokenLeft2 = token4,
                    TokenLeft1 = token5,
                    Token = token6,
                    TokenRight1 = token7,
                    TokenRight2 = token8,
                    TokenRight3 = token9
                },
                new()
                {
                    TokenLeft6 = token1,
                    TokenLeft5 = token2,
                    TokenLeft4 = token3,
                    TokenLeft3 = token4,
                    TokenLeft2 = token5,
                    TokenLeft1 = token6,
                    Token = token7,
                    TokenRight1 = token8,
                    TokenRight2 = token9
                },
                new()
                {
                    TokenLeft7 = token1,
                    TokenLeft6 = token2,
                    TokenLeft5 = token3,
                    TokenLeft4 = token4,
                    TokenLeft3 = token5,
                    TokenLeft2 = token6,
                    TokenLeft1 = token7,
                    Token = token8,
                    TokenRight1 = token9
                },
                new()
                {
                    TokenLeft8 = token1,
                    TokenLeft7 = token2,
                    TokenLeft6 = token3,
                    TokenLeft5 = token4,
                    TokenLeft4 = token5,
                    TokenLeft3 = token6,
                    TokenLeft2 = token7,
                    TokenLeft1 = token8,
                    Token = token9
                }
            };

            Assert.Equal(expected, concordances, new ConcordanceTokensComparer());
        }
    }
}