using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Services.Framework.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gos.Services.Framework
{
    public class GosDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public GosDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<CorpusCounter> CorpusCounters { get; set; }

        public DbSet<CorpusForm> CorpusForms { get; set; }

        public DbSet<DiscourseChannel> DiscourseChannels { get; set; }

        public DbSet<DiscourseEvent> DiscourseEvents { get; set; }

        public DbSet<DiscourseRegion> DiscourseRegions { get; set; }

        public DbSet<Discourse> Discourses { get; set; }

        public DbSet<DiscourseType> DiscourseTypes { get; set; }

        public DbSet<Msd> Msds { get; set; }

        public DbSet<PartOfSpeech> PartOfSpeeches { get; set; }

        public DbSet<Segment> Segments { get; set; }

        public DbSet<SpeakerAge> SpeakerAges { get; set; }

        public DbSet<SpeakerEducation> SpeakerEducations { get; set; }

        public DbSet<SpeakerLanguage> SpeakerLanguages { get; set; }

        public DbSet<SpeakerRegion> SpeakerRegions { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SpeakerSex> SpeakerSexes { get; set; }

        public DbSet<Statement> Statements { get; set; }

        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = configuration[ConfigurationKey.Database.ConnectionString];
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Setup entities for localization
            modelBuilder.Entity<DiscourseChannel>().Ignore(x => x.Title);
            modelBuilder.Entity<DiscourseEvent>().Ignore(x => x.Title);
            modelBuilder.Entity<DiscourseRegion>().Ignore(x => x.ShortTitle).Ignore(x => x.Title);
            modelBuilder.Entity<DiscourseType>().Ignore(x => x.ShortTitle).Ignore(x => x.Title);
            modelBuilder.Entity<Msd>().Ignore(x => x.Title);
            modelBuilder.Entity<PartOfSpeech>().Ignore(x => x.Title);
            modelBuilder.Entity<PartOfSpeechAttribute>().Ignore(x => x.Title);
            modelBuilder.Entity<PartOfSpeechAttributeValue>().Ignore(x => x.Title);
            modelBuilder.Entity<SpeakerAge>().Ignore(x => x.Title);
            modelBuilder.Entity<SpeakerEducation>().Ignore(x => x.Title);
            modelBuilder.Entity<SpeakerLanguage>().Ignore(x => x.Title);
            modelBuilder.Entity<SpeakerRegion>().Ignore(x => x.ShortTitle).Ignore(x => x.Title);
            modelBuilder.Entity<SpeakerSex>().Ignore(x => x.Title);
        }

        public async Task SeedDataAsync()
        {
            async Task SeedSingle<TEntity>(DbSet<TEntity> dbSet, IEnumerable<TEntity> entities)
                where TEntity : class
            {
                dbSet.AddRange(entities);
                await SaveChangesAsync();
            }

            await SeedSingle(DiscourseChannels, DiscourseChannelSeedData.Get());
            await SeedSingle(DiscourseEvents, DiscourseEventSeedData.Get());
            await SeedSingle(DiscourseRegions, DiscourseRegionSeedData.Get());
            await SeedSingle(DiscourseTypes, DiscourseTypeSeedData.Get());
            await SeedSingle(Msds, MsdSeedData.Get());
            await SeedSingle(PartOfSpeeches, PartOfSpeechSeedData.Get());
            await SeedSingle(SpeakerAges, SpeakerAgeSeedData.Get());
            await SeedSingle(SpeakerEducations, SpeakerEducationSeedData.Get());
            await SeedSingle(SpeakerLanguages, SpeakerLanguageSeedData.Get());
            await SeedSingle(SpeakerRegions, SpeakerRegionSeedData.Get());
            await SeedSingle(SpeakerSexes, SpeakerSexSeedData.Get());
        }
    }
}