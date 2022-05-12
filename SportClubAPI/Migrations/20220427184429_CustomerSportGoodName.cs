using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportClubAPI.Migrations
{
    public partial class CustomerSportGoodName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SportGoodName",
                table: "CustomerSportGoods",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportGoodName",
                table: "CustomerSportGoods");
        }
    }
}
