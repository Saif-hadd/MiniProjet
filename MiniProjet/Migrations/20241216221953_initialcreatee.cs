using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjet.Migrations
{
    /// <inheritdoc />
    public partial class initialcreatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Articles_ArticleId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_AspNetUsers_TechnicienId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions");

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Articles_ArticleId",
                table: "Interventions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_AspNetUsers_TechnicienId",
                table: "Interventions",
                column: "TechnicienId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions",
                column: "ReclamationId",
                principalTable: "Reclamations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Articles_ArticleId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_AspNetUsers_TechnicienId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions");

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Articles_ArticleId",
                table: "Interventions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_AspNetUsers_TechnicienId",
                table: "Interventions",
                column: "TechnicienId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Reclamations_ReclamationId",
                table: "Interventions",
                column: "ReclamationId",
                principalTable: "Reclamations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
