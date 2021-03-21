using Loja.Dados.Contexto;
using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Dados.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private LojaContexto _contexto;

        public ProdutoRepositorio(LojaContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Produto produto)
        {
            _contexto.Produtos.Update(produto);
        }

        public void Excluir(Produto produto)
        {
            _contexto.Produtos.Remove(produto);
        }

        public void Incluir(Produto produto)
        {
            _contexto.Produtos.Add(produto);
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            return await _contexto.Produtos.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            return await _contexto.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
