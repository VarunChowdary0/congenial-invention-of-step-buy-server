using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace step_buy_server.Migrations
{
    /// <inheritdoc />
    public partial class Change5343 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartOf",
                table: "OrderItems",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CartOf",
                table: "CartItems",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderItems",
                newName: "CartOf");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CartItems",
                newName: "CartOf");
        }
    }
}
