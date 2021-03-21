using Loja.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Repositorios
{
    public interface IPedidoRepositorio
    {
        Task<List<Pedido>> ObterPedidos();
        Task<List<PedidoItem>> ObterItensPedidos(int pedidoId);
        Task<Pedido> ObterPedidoPorId(int id);
        void Incluir(Pedido pedido);
        void Atualizar(Pedido pedido);
        void Excluir(Pedido pedido);
        void ExcluirItem(PedidoItem pedidoItem);
    }
}
