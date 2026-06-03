using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class FixAllCreatedDateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Profiles",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfileMatches",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfileLikes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Attachments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Profiles",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfileMatches",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfileLikes",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Attachments",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
