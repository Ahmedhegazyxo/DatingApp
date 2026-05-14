using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class fixProfilePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileId",
                table: "ProfilePhotos",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_Profiles_ProfileId",
                table: "ProfilePhotos",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
