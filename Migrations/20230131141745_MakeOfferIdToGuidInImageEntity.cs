using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bgbrokersapi.Migrations
{
    public partial class MakeOfferIdToGuidInImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OfferId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_OfferId",
                table: "Images",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Offers_OfferId",
                table: "Images",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Offers_OfferId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_OfferId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferId1",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Images_OfferId1",
                table: "Images",
                column: "OfferId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Offers_OfferId1",
                table: "Images",
                column: "OfferId1",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
