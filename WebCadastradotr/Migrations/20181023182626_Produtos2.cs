using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCadastrador.Migrations
{
    public partial class Produtos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preço",
                table: "Produto",
                newName: "Preco");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fabricante",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produto",
                newName: "Preço");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produto",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Fabricante",
                table: "Produto",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
