using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC2.Data.Migrations
{
    public partial class reservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<string>(nullable: false),
                    BuyerName = table.Column<string>(nullable: true),
                    BuyerEmail = table.Column<string>(nullable: true),
                    BuyerPhone = table.Column<string>(nullable: true),
                    CarVinNumber = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
