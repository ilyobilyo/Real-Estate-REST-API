using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bgbrokersapi.Migrations
{
    public partial class AddBrokerInOfferEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrokerId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BrokerId",
                table: "Offers",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_BrokerId",
                table: "Offers",
                column: "BrokerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_BrokerId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_BrokerId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "Offers");
        }
    }
}
