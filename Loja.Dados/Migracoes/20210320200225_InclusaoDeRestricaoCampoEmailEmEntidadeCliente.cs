using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Dados.Migracoes
{
    public partial class InclusaoDeRestricaoCampoEmailEmEntidadeCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Email",
                table: "Cliente",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Email",
                table: "Cliente");
        }
    }
}
