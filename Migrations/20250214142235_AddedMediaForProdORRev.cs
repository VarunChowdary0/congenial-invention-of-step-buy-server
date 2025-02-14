using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace step_buy_server.Migrations
{
    /// <inheritdoc />
    public partial class AddedMediaForProdORRev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaFor",
                table: "Media",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaFor",
                table: "Media");
        }
    }
}
