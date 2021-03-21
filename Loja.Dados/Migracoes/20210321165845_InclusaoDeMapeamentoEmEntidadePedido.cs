using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Dados.Migracoes
{
    public partial class InclusaoDeMapeamentoEmEntidadePedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Numero",
                table: "Pedido",
                column: "Numero",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pedido_Numero",
                table: "Pedido");
        }
    }
}
