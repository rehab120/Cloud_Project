using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryEdit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Delivery");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Delivery",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
