using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderApi.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
