using Gos.Core.Entities;
using Gos.Core.Model;

namespace Gos.Core.Extensions
{
    public static class TokenExtensions
    {
        public static Token WithConversationForm(this Token token, string conversationalForm)
        {
            token.ConversationalForm = conversationalForm?.Trim();
            return token;
        }

        public static Token WithLemma(this Token token, string lemma)
        {
            token.Lemma = lemma?.Trim();
            return token;
        }

        public static Token WithMsd(this Token token, string msd)
        {
            if (!string.IsNullOrEmpty(msd) && msd.ToLower().StartsWith("mte:"))
            {
                msd = msd[4..];
            }

            token.Msd = msd;
            return token;
        }

        public static Token WithStandardForm(this Token token, string standardForm)
        {
            token.StandardForm = standardForm?.Trim();
            return token;
        }

        public static Token WithType(this Token token, TokenType type)
        {
            token.Type = type;
            return token;
        }
    }
}