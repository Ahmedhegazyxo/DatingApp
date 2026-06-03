using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingCreatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2026, 5, 21, 11, 11, 45, 735, DateTimeKind.Utc).AddTicks(7785),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2026, 5, 21, 11, 11, 45, 735, DateTimeKind.Utc).AddTicks(7785));
        }
    }
}
