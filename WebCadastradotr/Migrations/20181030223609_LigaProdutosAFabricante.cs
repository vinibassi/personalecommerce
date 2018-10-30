using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCadastrador.Migrations
{
    public partial class LigaProdutosAFabricante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fabricante",
                table: "Produto");

            migrationBuilder.AddColumn<int>(
                name: "FabricanteId",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FabricanteId",
                table: "Produto",
                column: "FabricanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Fabricante_FabricanteId",
                table: "Produto",
                column: "FabricanteId",
                principalTable: "Fabricante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Fabricante_FabricanteId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_FabricanteId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "FabricanteId",
                table: "Produto");

            migrationBuilder.AddColumn<string>(
                name: "Fabricante",
                table: "Produto",
                nullable: false,
                defaultValue: "");
        }
    }
}
