using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagerApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix1InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Applications");

            migrationBuilder.CreateTable(
                name: "UsersMemberOfApplications",
                columns: table => new
                {
                    MemberOfId = table.Column<int>(type: "integer", nullable: false),
                    MembersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMemberOfApplications", x => new { x.MemberOfId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_UsersMemberOfApplications_Applications_MemberOfId",
                        column: x => x.MemberOfId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersMemberOfApplications_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_OwnerId",
                table: "Applications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMemberOfApplications_MembersId",
                table: "UsersMemberOfApplications",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_OwnerId",
                table: "Applications",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_OwnerId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "UsersMemberOfApplications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_OwnerId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Applications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
