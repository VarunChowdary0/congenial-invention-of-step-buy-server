using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace step_buy_server.Migrations
{
    /// <inheritdoc />
    public partial class RDTFBHUNJBVYTFCVGBYHU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Products");
        }
    }
}
