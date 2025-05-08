using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDeliverName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryName",
                table: "Package");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryName",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
