using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCadastrador.Migrations
{
    public partial class AlteraCadastroProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Produto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Produto");
        }
    }
}
