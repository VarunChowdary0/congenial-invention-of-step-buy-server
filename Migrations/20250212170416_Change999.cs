using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace step_buy_server.Migrations
{
    /// <inheritdoc />
    public partial class Change999 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDescription",
                table: "DeliveryInstructions");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryId",
                table: "DeliveryInstructions",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DeliveryInstructions",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryInstructions",
                table: "DeliveryInstructions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInstructions_DeliveryId",
                table: "DeliveryInstructions",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInstructions_Deliveries_DeliveryId",
                table: "DeliveryInstructions",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInstructions_Deliveries_DeliveryId",
                table: "DeliveryInstructions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryInstructions",
                table: "DeliveryInstructions");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryInstructions_DeliveryId",
                table: "DeliveryInstructions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeliveryInstructions");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryId",
                table: "DeliveryInstructions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryDescription",
                table: "DeliveryInstructions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
