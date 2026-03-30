using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagerApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTicketContentField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tickets",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Tickets",
                newName: "Description");
        }
    }
}
