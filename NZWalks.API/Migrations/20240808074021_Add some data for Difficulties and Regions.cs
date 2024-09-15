using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class AddsomedataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("108acb13-dab1-457d-9e58-58c0b4c808cd"), "Medium" },
                    { new Guid("a4c936b6-e014-4415-bebc-c44d9ac50374"), "Easy" },
                    { new Guid("fdba5f1c-c9bd-4db0-a8e9-ad42fb22f364"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1536cfbf-7a2c-4a45-aba8-693d56ab98c6"), "AZ", "Auckland", null },
                    { new Guid("5b6589e6-d476-494f-8b22-f35788c977f7"), "WK", "Waikato", null },
                    { new Guid("9d6e02aa-4f37-4712-8810-359d4c49ff0c"), "NL", "Northland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("108acb13-dab1-457d-9e58-58c0b4c808cd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a4c936b6-e014-4415-bebc-c44d9ac50374"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fdba5f1c-c9bd-4db0-a8e9-ad42fb22f364"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1536cfbf-7a2c-4a45-aba8-693d56ab98c6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5b6589e6-d476-494f-8b22-f35788c977f7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9d6e02aa-4f37-4712-8810-359d4c49ff0c"));
        }
    }
}
