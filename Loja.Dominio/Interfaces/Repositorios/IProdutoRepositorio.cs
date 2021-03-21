using Loja.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Repositorios
{
    public interface IProdutoRepositorio
    {
        Task<List<Produto>> ObterProdutos();
        Task<Produto> ObterProdutoPorId(int id);
        void Incluir(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(Produto produto);
    }
}
