using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using Loja.Dominio.Servicos;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Loja.Dominio.Testes.Servicos
{
    public class ProdutoServicoTeste
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProdutoServico _produtoServico;
        private Produto _produto;

        public ProdutoServicoTeste()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _produtoRepositorio = Substitute.For<IProdutoRepositorio>();
            _produtoServico = new ProdutoServico(_produtoRepositorio, _unitOfWork);
            _produto = new Produto() { Id = 1,  Descricao = "Artefato A", Valor = 2 };
        }

        [Fact]
        public async Task CriarProdutoRetornoComSucesso()
        {

            // Arrange
            _produtoRepositorio.Incluir(_produto);

            // Act
            var resultado = await _produtoServico.Adicionar(_produto);

            // Assert
            Assert.False(resultado.HasErrors());

        }

        [Fact]
        public async Task CriarProdutoRetornoComFalha()
        {
            // Arrange
            Produto produtoValorInvalido = new Produto() { 
                Descricao = "Artefato B",
                Valor = 0
            };

            // Act
            var resultado = await _produtoServico.Adicionar(produtoValorInvalido);

            // Assert
            Assert.True(resultado.HasErrors());

        }
                

        [Fact]
        public async Task ExcluirProdutoNaoEncontradoRetornoComFalha()
        {
            // Arrange
            Produto produtoExistente = null;

            _produtoRepositorio.ObterProdutoPorId(_produto.Id)
               .Returns(produtoExistente);

            // Act
            var resultado = await _produtoServico.Excluir(_produto.Id);

            // Assert
            Assert.True(resultado.HasErrors());

        }

        [Fact]
        public async Task ExcluirProdutoEncontradoRetornoComSucesso()
        {
            // Arrange
            _produtoRepositorio.ObterProdutoPorId(_produto.Id)
               .Returns(_produto);

            _produtoRepositorio.Excluir(_produto);

            // Act
            var resultado = await _produtoServico.Excluir(_produto.Id);

            // Assert
            Assert.False(resultado.HasErrors());

        }
    }
}
