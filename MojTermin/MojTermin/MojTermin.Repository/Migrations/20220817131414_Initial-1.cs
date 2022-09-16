using Microsoft.EntityFrameworkCore.Migrations;

namespace MojTermin.Repository.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisInVisits_Visit_VisitId",
                table: "DiagnosisInVisits");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisInVisits_VisitId",
                table: "DiagnosisInVisits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisInVisits_VisitId",
                table: "DiagnosisInVisits",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisInVisits_Visit_VisitId",
                table: "DiagnosisInVisits",
                column: "VisitId",
                principalTable: "Visit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
