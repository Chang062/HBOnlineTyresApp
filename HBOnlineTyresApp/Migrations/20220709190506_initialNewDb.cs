using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HBOnlineTyresApp.Migrations
{
    public partial class initialNewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tyres_CategoryId",
                table: "Tyres",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tyres_Categories_CategoryId",
                table: "Tyres",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tyres_Categories_CategoryId",
                table: "Tyres");

            migrationBuilder.DropIndex(
                name: "IX_Tyres_CategoryId",
                table: "Tyres");
        }
    }
}
