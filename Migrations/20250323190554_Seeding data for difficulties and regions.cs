using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("053c1043-6fb4-41b9-9d3c-26a498a1413d"), "Easy" },
                    { new Guid("9a3de88c-1a60-4c91-9461-af43cacd1005"), "Hard" },
                    { new Guid("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1b8d43d6-21ca-40b2-9c20-85a4e8750475"), "", "Tasman", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg" },
                    { new Guid("4bf96fb5-4419-404b-96b2-3b894d0ba2f5"), "", "Hawke's Bay", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg" },
                    { new Guid("5025788f-9ab6-4778-bdc6-46623d1e8de5"), "", "Gisborne", null },
                    { new Guid("5db28a12-9b19-4046-b6ad-01dc5a63b7de"), "", "Bay of Plenty", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg" },
                    { new Guid("9a3de88c-1a60-4c91-9461-af43cacd1005"), "", "Waikato", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg" },
                    { new Guid("9c11f8b5-252f-4808-a2b3-726d1d26f311"), "", "Manawatu-Wanganui", null },
                    { new Guid("b39553be-de94-4d3b-a231-0ede8cf20a6e"), "", "Nelson", null },
                    { new Guid("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"), "", "Auckland", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg" },
                    { new Guid("b92ebf56-42fc-412b-9732-247a1921165c"), "", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg" },
                    { new Guid("f3b3b3b3-6fb4-41b9-9d3c-26a498a1413d"), "", "Northland", null },
                    { new Guid("fed42b58-fd6a-4dad-a57f-4cb452ba4a8f"), "", "Taranaki", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("053c1043-6fb4-41b9-9d3c-26a498a1413d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9a3de88c-1a60-4c91-9461-af43cacd1005"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1b8d43d6-21ca-40b2-9c20-85a4e8750475"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4bf96fb5-4419-404b-96b2-3b894d0ba2f5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5025788f-9ab6-4778-bdc6-46623d1e8de5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5db28a12-9b19-4046-b6ad-01dc5a63b7de"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9a3de88c-1a60-4c91-9461-af43cacd1005"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9c11f8b5-252f-4808-a2b3-726d1d26f311"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b39553be-de94-4d3b-a231-0ede8cf20a6e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b92ebf56-42fc-412b-9732-247a1921165c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f3b3b3b3-6fb4-41b9-9d3c-26a498a1413d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fed42b58-fd6a-4dad-a57f-4cb452ba4a8f"));
        }
    }
}
