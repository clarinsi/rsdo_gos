using System;

namespace Gos.Core
{
    public static class Cache
    {
        public static class Duration
        {
            public static TimeSpan Long = TimeSpan.FromDays(7);
        }

        public static class CacheKeys
        {
            public static class Msd
            {
                private const string Prefix = nameof(Msd);

                public static string All()
                {
                    return $"{Prefix}_{nameof(All)}";
                }

                public static string ByCode(string code)
                {
                    return $"{Prefix}_{nameof(ByCode)}_{code}";
                }
            }

            public static class PartOfSpeech
            {
                private const string Prefix = nameof(PartOfSpeech);

                public static string ByCode(string code)
                {
                    return $"{Prefix}_{nameof(ByCode)}_{code}";
                }
            }
        }
    }
}