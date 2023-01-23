using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gos.Services.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorpusCounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discourses = table.Column<int>(type: "integer", nullable: false),
                    Statements = table.Column<int>(type: "integer", nullable: false),
                    Words = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpusCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CorpusForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StandardForm = table.Column<string>(type: "text", nullable: true),
                    Lemma = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorpusForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Msds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Msds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeeches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    RecordOrder = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeeches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerAges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerAges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerEducations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerEducations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseChannelTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DiscourseChannelId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseChannelTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscourseChannelTranslation_DiscourseChannels_DiscourseChan~",
                        column: x => x.DiscourseChannelId,
                        principalTable: "DiscourseChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseEventTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DiscourseEventId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseEventTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscourseEventTranslation_DiscourseEvents_DiscourseEventId",
                        column: x => x.DiscourseEventId,
                        principalTable: "DiscourseEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseRegionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShortTitle = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DiscourseRegionId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseRegionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscourseRegionTranslation_DiscourseRegions_DiscourseRegion~",
                        column: x => x.DiscourseRegionId,
                        principalTable: "DiscourseRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    NumberOfSpeakers = table.Column<int>(type: "integer", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discourses_DiscourseChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "DiscourseChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discourses_DiscourseEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "DiscourseEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discourses_DiscourseRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "DiscourseRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discourses_DiscourseTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DiscourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscourseTypeTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShortTitle = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    DiscourseTypeId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscourseTypeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscourseTypeTranslation_DiscourseTypes_DiscourseTypeId",
                        column: x => x.DiscourseTypeId,
                        principalTable: "DiscourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MsdTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    MsdId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsdTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MsdTranslation_Msds_MsdId",
                        column: x => x.MsdId,
                        principalTable: "Msds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeechAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecordOrder = table.Column<short>(type: "smallint", nullable: false),
                    PartOfSpeechId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeechAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeechAttribute_PartOfSpeeches_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeechTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PartOfSpeechId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeechTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeechTranslation_PartOfSpeeches_PartOfSpeechId",
                        column: x => x.PartOfSpeechId,
                        principalTable: "PartOfSpeeches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerAgeTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SpeakerAgeId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerAgeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerAgeTranslation_SpeakerAges_SpeakerAgeId",
                        column: x => x.SpeakerAgeId,
                        principalTable: "SpeakerAges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerEducationTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SpeakerEducationId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerEducationTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerEducationTranslation_SpeakerEducations_SpeakerEducat~",
                        column: x => x.SpeakerEducationId,
                        principalTable: "SpeakerEducations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerLanguageTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SpeakerLanguageId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerLanguageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerLanguageTranslation_SpeakerLanguages_SpeakerLanguage~",
                        column: x => x.SpeakerLanguageId,
                        principalTable: "SpeakerLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerRegionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShortTitle = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SpeakerRegionId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerRegionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerRegionTranslation_SpeakerRegions_SpeakerRegionId",
                        column: x => x.SpeakerRegionId,
                        principalTable: "SpeakerRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgeId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    EducationId = table.Column<int>(type: "integer", nullable: true),
                    LanguageId = table.Column<int>(type: "integer", nullable: true),
                    Region1Id = table.Column<int>(type: "integer", nullable: true),
                    Region2Id = table.Column<int>(type: "integer", nullable: true),
                    Region3Id = table.Column<int>(type: "integer", nullable: true),
                    SexId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerAges_AgeId",
                        column: x => x.AgeId,
                        principalTable: "SpeakerAges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerEducations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "SpeakerEducations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "SpeakerLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerRegions_Region1Id",
                        column: x => x.Region1Id,
                        principalTable: "SpeakerRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerRegions_Region2Id",
                        column: x => x.Region2Id,
                        principalTable: "SpeakerRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerRegions_Region3Id",
                        column: x => x.Region3Id,
                        principalTable: "SpeakerRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Speakers_SpeakerSexes_SexId",
                        column: x => x.SexId,
                        principalTable: "SpeakerSexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSexTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SpeakerSexId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSexTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerSexTranslation_SpeakerSexes_SpeakerSexId",
                        column: x => x.SpeakerSexId,
                        principalTable: "SpeakerSexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeechAttributeTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PartOfSpeechAttributeId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeechAttributeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeechAttributeTranslation_PartOfSpeechAttribute_Part~",
                        column: x => x.PartOfSpeechAttributeId,
                        principalTable: "PartOfSpeechAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeechAttributeValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    RecordOrder = table.Column<short>(type: "smallint", nullable: false),
                    PartOfSpeechAttributeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeechAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeechAttributeValue_PartOfSpeechAttribute_PartOfSpee~",
                        column: x => x.PartOfSpeechAttributeId,
                        principalTable: "PartOfSpeechAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    DiscourseId = table.Column<int>(type: "integer", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    SpeakerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statements_Discourses_DiscourseId",
                        column: x => x.DiscourseId,
                        principalTable: "Discourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeechAttributeValueTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PartOfSpeechAttributeValueId = table.Column<int>(type: "integer", nullable: true),
                    CultureName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeechAttributeValueTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartOfSpeechAttributeValueTranslation_PartOfSpeechAttribute~",
                        column: x => x.PartOfSpeechAttributeValueId,
                        principalTable: "PartOfSpeechAttributeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    SoundFile = table.Column<string>(type: "text", nullable: true),
                    StatementId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_Statements_StatementId",
                        column: x => x.StatementId,
                        principalTable: "Statements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversationalForm = table.Column<string>(type: "text", nullable: true),
                    DiscourseOrder = table.Column<int>(type: "integer", nullable: false),
                    Lemma = table.Column<string>(type: "text", nullable: true),
                    Msd = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    SegmentId = table.Column<int>(type: "integer", nullable: true),
                    StandardForm = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscourseChannelTranslation_DiscourseChannelId",
                table: "DiscourseChannelTranslation",
                column: "DiscourseChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscourseEventTranslation_DiscourseEventId",
                table: "DiscourseEventTranslation",
                column: "DiscourseEventId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscourseRegionTranslation_DiscourseRegionId",
                table: "DiscourseRegionTranslation",
                column: "DiscourseRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discourses_ChannelId",
                table: "Discourses",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Discourses_EventId",
                table: "Discourses",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Discourses_RegionId",
                table: "Discourses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discourses_TypeId",
                table: "Discourses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscourseTypeTranslation_DiscourseTypeId",
                table: "DiscourseTypeTranslation",
                column: "DiscourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MsdTranslation_MsdId",
                table: "MsdTranslation",
                column: "MsdId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeechAttribute_PartOfSpeechId",
                table: "PartOfSpeechAttribute",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeechAttributeTranslation_PartOfSpeechAttributeId",
                table: "PartOfSpeechAttributeTranslation",
                column: "PartOfSpeechAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeechAttributeValue_PartOfSpeechAttributeId",
                table: "PartOfSpeechAttributeValue",
                column: "PartOfSpeechAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeechAttributeValueTranslation_PartOfSpeechAttribute~",
                table: "PartOfSpeechAttributeValueTranslation",
                column: "PartOfSpeechAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PartOfSpeechTranslation_PartOfSpeechId",
                table: "PartOfSpeechTranslation",
                column: "PartOfSpeechId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_StatementId",
                table: "Segments",
                column: "StatementId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerAgeTranslation_SpeakerAgeId",
                table: "SpeakerAgeTranslation",
                column: "SpeakerAgeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerEducationTranslation_SpeakerEducationId",
                table: "SpeakerEducationTranslation",
                column: "SpeakerEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerLanguageTranslation_SpeakerLanguageId",
                table: "SpeakerLanguageTranslation",
                column: "SpeakerLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerRegionTranslation_SpeakerRegionId",
                table: "SpeakerRegionTranslation",
                column: "SpeakerRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_AgeId",
                table: "Speakers",
                column: "AgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_EducationId",
                table: "Speakers",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_LanguageId",
                table: "Speakers",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_Region1Id",
                table: "Speakers",
                column: "Region1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_Region2Id",
                table: "Speakers",
                column: "Region2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_Region3Id",
                table: "Speakers",
                column: "Region3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_SexId",
                table: "Speakers",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSexTranslation_SpeakerSexId",
                table: "SpeakerSexTranslation",
                column: "SpeakerSexId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_DiscourseId",
                table: "Statements",
                column: "DiscourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_SpeakerId",
                table: "Statements",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_SegmentId",
                table: "Tokens",
                column: "SegmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorpusCounters");

            migrationBuilder.DropTable(
                name: "CorpusForms");

            migrationBuilder.DropTable(
                name: "DiscourseChannelTranslation");

            migrationBuilder.DropTable(
                name: "DiscourseEventTranslation");

            migrationBuilder.DropTable(
                name: "DiscourseRegionTranslation");

            migrationBuilder.DropTable(
                name: "DiscourseTypeTranslation");

            migrationBuilder.DropTable(
                name: "MsdTranslation");

            migrationBuilder.DropTable(
                name: "PartOfSpeechAttributeTranslation");

            migrationBuilder.DropTable(
                name: "PartOfSpeechAttributeValueTranslation");

            migrationBuilder.DropTable(
                name: "PartOfSpeechTranslation");

            migrationBuilder.DropTable(
                name: "SpeakerAgeTranslation");

            migrationBuilder.DropTable(
                name: "SpeakerEducationTranslation");

            migrationBuilder.DropTable(
                name: "SpeakerLanguageTranslation");

            migrationBuilder.DropTable(
                name: "SpeakerRegionTranslation");

            migrationBuilder.DropTable(
                name: "SpeakerSexTranslation");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Msds");

            migrationBuilder.DropTable(
                name: "PartOfSpeechAttributeValue");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "PartOfSpeechAttribute");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "PartOfSpeeches");

            migrationBuilder.DropTable(
                name: "Discourses");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "DiscourseChannels");

            migrationBuilder.DropTable(
                name: "DiscourseEvents");

            migrationBuilder.DropTable(
                name: "DiscourseRegions");

            migrationBuilder.DropTable(
                name: "DiscourseTypes");

            migrationBuilder.DropTable(
                name: "SpeakerAges");

            migrationBuilder.DropTable(
                name: "SpeakerEducations");

            migrationBuilder.DropTable(
                name: "SpeakerLanguages");

            migrationBuilder.DropTable(
                name: "SpeakerRegions");

            migrationBuilder.DropTable(
                name: "SpeakerSexes");
        }
    }
}
