using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Delivery");

            migrationBuilder.AddColumn<string>(
                name: "Delivery_id",
                table: "Package",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_Delivery_id",
                table: "Package",
                column: "Delivery_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Delivery_Delivery_id",
                table: "Package",
                column: "Delivery_id",
                principalTable: "Delivery",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Delivery_Delivery_id",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_Delivery_id",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "Delivery_id",
                table: "Package");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Delivery",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Delivery",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
