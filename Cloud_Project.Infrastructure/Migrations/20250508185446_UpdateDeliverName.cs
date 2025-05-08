using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeliverName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Delivery_Delivery_id",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "Delivery_id",
                table: "Package",
                newName: "DeliveryId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_Delivery_id",
                table: "Package",
                newName: "IX_Package_DeliveryId");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryName",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Delivery_DeliveryId",
                table: "Package",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Delivery_DeliveryId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "DeliveryName",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                table: "Package",
                newName: "Delivery_id");

            migrationBuilder.RenameIndex(
                name: "IX_Package_DeliveryId",
                table: "Package",
                newName: "IX_Package_Delivery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Delivery_Delivery_id",
                table: "Package",
                column: "Delivery_id",
                principalTable: "Delivery",
                principalColumn: "Id");
        }
    }
}
