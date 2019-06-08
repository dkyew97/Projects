using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC2.Data.Migrations
{
    public partial class Cars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    CarVinNumber = table.Column<string>(nullable: false),
                    CarName = table.Column<string>(nullable: true),
                    CarMake = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.CarVinNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarModel");
        }
    }
}
