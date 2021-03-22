using Loja.Dados.Repositorios;
using Loja.Dominio.Entidades;
using System.Threading.Tasks;
using Xunit;

namespace Loja.Dados.Testes.Repositorios
{
    public class ProdutoRepositorioTestes
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly UnitOfWork _unitOfWork;

        public ProdutoRepositorioTestes()
        {
            var bd = new BdMemoria();
            var contexto = bd.ObterContexto();
            _produtoRepositorio = new ProdutoRepositorio(contexto);
            _unitOfWork = new UnitOfWork(contexto);
        }

        [Fact]
        public async Task ObterProdutos()
        {
            // Act
            var produtos = await _produtoRepositorio.ObterProdutos();

            // Assert
            Assert.Equal(2, produtos.Count);
        }


        [Fact]
        public async Task ObterProdutoPorId()
        {
            // Act
            var cliente = await _produtoRepositorio.ObterProdutoPorId(1);

            // Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public async Task IncluirProduto()
        {
            // Arrange
            _produtoRepositorio.Incluir(new Produto()
            {
                Descricao = "Produto 3",
                Valor = 50
            });

            await _unitOfWork.Commit();

            // Act
            var produto = await _produtoRepositorio.ObterProdutoPorId(3);

            // Assert
            Assert.NotNull(produto);
        }
    }
}
