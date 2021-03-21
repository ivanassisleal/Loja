using Loja.Dominio.Entidades;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Servicos
{
    public interface IProdutoServico
    {
        Task<Resultado> Adicionar(Produto produto);
        Task<Resultado> Atualizar(Produto produto);
        Task<Resultado> Excluir(int id);
    }
}
