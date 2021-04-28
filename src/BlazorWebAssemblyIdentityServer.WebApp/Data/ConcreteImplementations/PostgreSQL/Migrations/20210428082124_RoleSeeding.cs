using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL.Migrations
{
    public partial class RoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "bbbbc82f-079b-460a-9096-c2025a3cdbbd", "Administrators", "ADMINISTRATORS" },
                    { 2L, "8b2c1d56-cfd3-4d20-9091-84d8143de571", "Supervisors", "SUPERVISORS" },
                    { 3L, "6014a4e7-1a15-4465-a04f-71194a1d55c1", "Leaders", "LEADERS" },
                    { 4L, "9358438d-a0d1-4fd6-912c-1b7535a06af2", "Limited", "LIMITED" },
                    { 5L, "62580c05-1a60-472d-9392-f05e2c63fc2e", "Banned", "BANNED" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
