namespace Gos.Core
{
    public static class ConfigurationKey
    {
        private const string Prefix = "GOS";

        public static string SoundFilesFolder => $"{Prefix}:{nameof(SoundFilesFolder)}";

        public static class Database
        {
            public static string ConnectionString => $"{Prefix}:{nameof(Database)}:{nameof(ConnectionString)}";
        }

        public static class Elastic
        {
            public static string ConnectionString => $"{Prefix}:{nameof(Elastic)}:{nameof(ConnectionString)}";
        }

        public static class Logging
        {
            public static string LogPath => $"{Prefix}:{nameof(Logging)}:{nameof(LogPath)}";
        }

        public static class Web
        {
            public static string ProxyBasePath => $"{Prefix}:{nameof(Web)}:{nameof(ProxyBasePath)}";
        }
    }
}