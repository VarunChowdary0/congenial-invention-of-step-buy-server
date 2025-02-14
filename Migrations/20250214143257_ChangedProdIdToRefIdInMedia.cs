using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace step_buy_server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProdIdToRefIdInMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Media",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ReferanceId",
                table: "Media",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ReferanceId",
                table: "Media");

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "ProductId",
                keyValue: null,
                column: "ProductId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Media",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Products_ProductId",
                table: "Media",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
