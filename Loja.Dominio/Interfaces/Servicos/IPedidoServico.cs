using Loja.Dominio.Entidades;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Servicos
{
    public interface IPedidoServico
    {
        Task<Resultado> Adicionar(Pedido pedido);
        Task<Resultado> Atualizar(Pedido pedido);
        Task<Resultado> Excluir(int id);
    }
}
