using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guacamole.Minimal.Migrations
{
    /// <inheritdoc />
    public partial class DateOnlyToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Ideas",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Ideas",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Categories",
                newName: "Created");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Ideas",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Ideas",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Categories",
                newName: "DateCreated");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateOnly(2022, 11, 17));

            migrationBuilder.UpdateData(
                table: "Ideas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateOnly(2022, 11, 17), new DateOnly(2022, 11, 17) });
        }
    }
}
