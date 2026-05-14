using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class fixRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId1",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePhotos_ProfileId1",
                table: "ProfilePhotos");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_Profiles_Id",
                table: "ProfilePhotos",
                column: "Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_Profiles_Id",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProfilePhotos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId1",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_ProfileId1",
                table: "ProfilePhotos",
                column: "ProfileId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId1",
                table: "ProfilePhotos",
                column: "ProfileId1",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}
