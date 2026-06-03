using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddingChatMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageMatchId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageBody = table.Column<string>(type: "TEXT", nullable: false),
                    SenderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ProfileMatches_MessageMatchId",
                        column: x => x.MessageMatchId,
                        principalTable: "ProfileMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Profiles_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_MessageMatchId",
                table: "ChatMessages",
                column: "MessageMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");
        }
    }
}
