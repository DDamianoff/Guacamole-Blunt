using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guacamole.Minimal.Migrations
{
    /// <inheritdoc />
    public partial class CategoryCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 17, 11, 8, 29, 219, DateTimeKind.Local).AddTicks(6012));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 17, 11, 8, 29, 219, DateTimeKind.Local).AddTicks(6207), new DateTime(2022, 11, 17, 11, 8, 29, 219, DateTimeKind.Local).AddTicks(6210) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9446), new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9452) });
        }
    }
}
