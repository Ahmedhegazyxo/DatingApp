using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class EditingAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Attachments",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "RootPath",
                table: "Attachments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootPath",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachments",
                newName: "URL");
        }
    }
}
