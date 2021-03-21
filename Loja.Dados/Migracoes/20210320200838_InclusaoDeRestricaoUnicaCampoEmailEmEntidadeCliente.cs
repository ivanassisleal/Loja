using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Dados.Migracoes
{
    public partial class InclusaoDeRestricaoUnicaCampoEmailEmEntidadeCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Email",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Email",
                table: "Cliente",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Email",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Email",
                table: "Cliente",
                column: "Email");
        }
    }
}
