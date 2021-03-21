using Loja.Dominio.Entidades;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Servicos
{
    public interface IClienteServico
    {
        Task<Resultado> Adicionar(Cliente cliente);
        Task<Resultado> Atualizar(Cliente cliente);
        Task<Resultado> Excluir(int id);
    }
}
