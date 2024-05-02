using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restuarant.Migrations
{
    public partial class editDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterSliderImageUrl",
                table: "MasterSlider",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterSliderImageUrl",
                table: "MasterSlider");
        }
    }
}
