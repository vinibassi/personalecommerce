using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCadastrador.Migrations
{
    public partial class TiraUnderscoreEstCivCli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado_Civil",
                table: "Clientes");

            migrationBuilder.AddColumn<byte>(
                name: "EstadoCivil",
                table: "Clientes",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoCivil",
                table: "Clientes");

            migrationBuilder.AddColumn<byte>(
                name: "Estado_Civil",
                table: "Clientes",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
