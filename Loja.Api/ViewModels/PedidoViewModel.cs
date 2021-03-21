using System.Collections.Generic;

namespace Loja.Api
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Desconto { get; set; }
        public List<PedidoItemViewModel> Produtos { get; set; }
    }

    public class PedidoItemViewModel
    {
        public int ProdutoId { get; set; }
    }
}
