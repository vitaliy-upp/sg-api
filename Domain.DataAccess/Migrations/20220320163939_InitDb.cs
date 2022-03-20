using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.DataAccess.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToDeskId = table.Column<int>(nullable: true),
                    ToUserId = table.Column<int>(nullable: true),
                    ToFloorId = table.Column<int>(nullable: true),
                    ToConferenceId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    FromUserId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripeProductsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(nullable: true),
                    PriceId = table.Column<string>(nullable: true),
                    ConferenceId = table.Column<int>(nullable: true),
                    SubscriptionPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeProductsInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Annual = table.Column<decimal>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    FreeTrialPeriod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperPowers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperPowers_SuperPowers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SuperPowers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileExtension = table.Column<string>(nullable: true),
                    MessageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachments_ChatMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    SubscriptionPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberValue = table.Column<decimal>(nullable: true),
                    FlagValue = table.Column<bool>(nullable: true),
                    FeatureId = table.Column<int>(nullable: false),
                    SubscriptionPlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionFeatures_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsEmailVerified = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KidProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParrentId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Impairment = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KidProfiles_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KidProfiles_Users_ParrentId",
                        column: x => x.ParrentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenKey = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserTokenType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KidProfileId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: true),
                    IsUpToPresent = table.Column<bool>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    AttachmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Educations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Educations_KidProfiles_KidProfileId",
                        column: x => x.KidProfileId,
                        principalTable: "KidProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuperPowerToKid",
                columns: table => new
                {
                    KidId = table.Column<int>(nullable: false),
                    SuperPowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperPowerToKid", x => new { x.KidId, x.SuperPowerId });
                    table.ForeignKey(
                        name: "FK_SuperPowerToKid_KidProfiles_KidId",
                        column: x => x.KidId,
                        principalTable: "KidProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuperPowerToKid_SuperPowers_SuperPowerId",
                        column: x => x.SuperPowerId,
                        principalTable: "SuperPowers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Key", "Name" },
                values: new object[] { 1, "key", "Name" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annual", "FreeTrialPeriod", "Name", "Price", "Type" },
                values: new object[] { 1, null, 1, "Organizer", 99m, 2 });

            migrationBuilder.InsertData(
                table: "SuperPowers",
                columns: new[] { "Id", "Icon", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, null, "Music", null },
                    { 19, null, "Dance", null },
                    { 33, null, "Fashion", null }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "FeatureId", "FlagValue", "NumberValue", "SubscriptionPlanId" },
                values: new object[] { 1, 1, null, 100m, 1 });

            migrationBuilder.InsertData(
                table: "SuperPowers",
                columns: new[] { "Id", "Icon", "Name", "ParentId" },
                values: new object[,]
                {
                    { 31, null, "Modern Dance", 19 },
                    { 30, null, "Irish Dance", 19 },
                    { 29, null, "Folk Dance", 19 },
                    { 28, null, "Tap Dance", 19 },
                    { 27, null, "Jazz", 19 },
                    { 26, null, "Contemporary", 19 },
                    { 25, null, "Ballroom", 19 },
                    { 24, null, "Ballet", 19 },
                    { 23, null, "Hip Hop", 19 },
                    { 22, null, "Dance troupe", 19 },
                    { 21, null, "Dance group", 19 },
                    { 20, null, "Solo dancer", 19 },
                    { 18, null, "Drummer", 1 },
                    { 32, null, "Swing Dance", 19 },
                    { 17, null, "DJ", 1 },
                    { 15, null, "Percussion", 1 },
                    { 14, null, "Orchestra", 1 },
                    { 13, null, "Marching band", 1 },
                    { 12, null, "Jazz Band", 1 },
                    { 11, null, "Choir", 1 },
                    { 10, null, "Singer. Lip sync", 1 },
                    { 9, null, "Singer. Opera singer", 1 },
                    { 8, null, "Singer. Country", 1 },
                    { 7, null, "Singer. Band", 1 },
                    { 6, null, "Singer. Country singer", 1 },
                    { 5, null, "Singer. Yodeler", 1 },
                    { 4, null, "Musician (playing an instrument). Drums", 1 },
                    { 3, null, "Musician (playing an instrument). Guitar", 1 },
                    { 16, null, "Song writer", 1 },
                    { 2, null, "Musician (playing an instrument). Piano", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SubscriptionPlanId",
                table: "Companies",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_AttachmentId",
                table: "Educations",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CityId",
                table: "Educations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_KidProfileId",
                table: "Educations",
                column: "KidProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_KidProfiles_CityId",
                table: "KidProfiles",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_KidProfiles_ParrentId",
                table: "KidProfiles",
                column: "ParrentId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachments_MessageId",
                table: "MessageAttachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionFeatures_FeatureId",
                table: "SubscriptionFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionFeatures_SubscriptionPlanId",
                table: "SubscriptionFeatures",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperPowers_ParentId",
                table: "SuperPowers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperPowerToKid_SuperPowerId",
                table: "SuperPowerToKid",
                column: "SuperPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "MessageAttachments");

            migrationBuilder.DropTable(
                name: "StripeProductsInfo");

            migrationBuilder.DropTable(
                name: "SubscriptionFeatures");

            migrationBuilder.DropTable(
                name: "SuperPowerToKid");

            migrationBuilder.DropTable(
                name: "UserCustomers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "KidProfiles");

            migrationBuilder.DropTable(
                name: "SuperPowers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
