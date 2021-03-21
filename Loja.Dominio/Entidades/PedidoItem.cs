using System;

namespace Loja.Dominio.Entidades
{
    public class PedidoItem
    {
        public Guid Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }

        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
    }
}
