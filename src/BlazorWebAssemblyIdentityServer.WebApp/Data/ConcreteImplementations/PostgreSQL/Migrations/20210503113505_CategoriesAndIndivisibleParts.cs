using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL.Migrations
{
    public partial class CategoriesAndIndivisibleParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetIndivisibleParts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PartSize = table.Column<double>(type: "double precision", nullable: false),
                    LastChangedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastChangedByUserId = table.Column<long>(type: "bigint", nullable: false),
                    OwnedAssetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetIndivisibleParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetIndivisibleParts_AspNetUsers_LastChangedByUserId",
                        column: x => x.LastChangedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetIndivisibleParts_OwnedAssets_OwnedAssetId",
                        column: x => x.OwnedAssetId,
                        principalTable: "OwnedAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetPartCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastChangedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastChangedByUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPartCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetPartCategories_AspNetUsers_LastChangedByUserId",
                        column: x => x.LastChangedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetPartCategoryAssociations",
                columns: table => new
                {
                    AssetIndivisiblePartId = table.Column<long>(type: "bigint", nullable: false),
                    AssetPartCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetPartCategoryAssociations", x => new { x.AssetIndivisiblePartId, x.AssetPartCategoryId });
                    table.ForeignKey(
                        name: "FK_AssetPartCategoryAssociations_AssetIndivisibleParts_AssetIn~",
                        column: x => x.AssetIndivisiblePartId,
                        principalTable: "AssetIndivisibleParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetPartCategoryAssociations_AssetPartCategories_AssetPart~",
                        column: x => x.AssetPartCategoryId,
                        principalTable: "AssetPartCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "0156e0ac-38e2-463f-8afa-67bc3a8db786");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "88476e44-e979-4bd1-9d0a-efd222525f7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "67e2f22b-a902-4dc9-be1c-f172d5cbcc6a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "f543cc9c-1830-4a60-87fe-0972e2c6004a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "6199fd85-d01a-4bd7-9b43-83882c4868dd");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIndivisibleParts_LastChangedByUserId",
                table: "AssetIndivisibleParts",
                column: "LastChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetIndivisibleParts_OwnedAssetId",
                table: "AssetIndivisibleParts",
                column: "OwnedAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPartCategories_LastChangedByUserId",
                table: "AssetPartCategories",
                column: "LastChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPartCategoryAssociations_AssetPartCategoryId",
                table: "AssetPartCategoryAssociations",
                column: "AssetPartCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetPartCategoryAssociations");

            migrationBuilder.DropTable(
                name: "AssetIndivisibleParts");

            migrationBuilder.DropTable(
                name: "AssetPartCategories");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ec69bda8-6914-4c69-8415-1267d6433c35");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "78616a13-82b9-4548-b7f8-ba3a8f0746e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "1f8d4896-5909-445e-bda4-8411344acc3d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ConcurrencyStamp",
                value: "bf98c40a-6bc9-4273-bbc2-c6aaaf364ba4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ConcurrencyStamp",
                value: "27d57ec3-856c-49d0-a9f3-5b971832be72");
        }
    }
}
