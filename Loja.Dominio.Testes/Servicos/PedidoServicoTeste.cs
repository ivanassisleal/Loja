using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using Loja.Dominio.Servicos;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Loja.Dominio.Testes.Servicos
{
    public class PedidoServicoTeste
    {
        private IPedidoRepositorio _pedidoRepositorio;
        private IProdutoRepositorio _produtoRepositorio;
        private IPedidoServico _pedidoServico;
        private IUnitOfWork _unitOfWork;
        private Produto _produto;

        public PedidoServicoTeste()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _pedidoRepositorio = Substitute.For<IPedidoRepositorio>();
            _produtoRepositorio = Substitute.For<IProdutoRepositorio>();
            _pedidoServico = new PedidoServico(_produtoRepositorio, _pedidoRepositorio, _unitOfWork);
            _produto = new Produto() { Id = 1, Descricao = "Artefato A", Valor = 23 };
        }

        [Fact]
        public async Task AdicionarPedidoSemProdutoRetornoComFalha()
        {
            // Arrange
            Pedido pedidoSemProdutos = new Pedido()
            {
                Id = 1,
            };

            // Act
            var resultado = await _pedidoServico.Adicionar(pedidoSemProdutos);

            // Assert
            var erro = resultado.GetErrors().FirstOrDefault().Message;
            Assert.Equal("Pedido precisa ter pelo menos um produto",erro);
        }

        [Fact]
        public async Task AdicionarPedidoProdutoInformadoNaoExisteRetornoComFalha()
        {
            // Arrange
            Pedido pedidoComProdutos = new Pedido()
            {
                Id = 1,
            };

            pedidoComProdutos.Produtos.Add(new PedidoItem()
            {
                Id = Guid.NewGuid(),
                PedidoId = 1,
                ProdutoId = 2
            });

            Produto produtoInexistente = null;

            _produtoRepositorio.ObterProdutoPorId(_produto.Id)
                .Returns(produtoInexistente);

            // Act
            var resultado = await _pedidoServico.Adicionar(pedidoComProdutos);

            // Assert
            var erro = resultado.GetErrors().FirstOrDefault().Message;
            Assert.Equal("Produto Informado no pedido não encontrado", erro);
        }

        [Fact]
        public async Task AdicionarPedidoRetornoComSucesso()
        {
            // Arrange
            Pedido pedidoComProdutos = new Pedido()
            {
                Id = 1,
                ClienteId = 1,
                Numero = 1,
                Desconto = 3,
            };

            pedidoComProdutos.Produtos.Add(new PedidoItem()
            {
                Id = Guid.NewGuid(),
                PedidoId = 1,
                ProdutoId = 1
            });

            List<Pedido> pedidos = new List<Pedido>();
            pedidos.Add(pedidoComProdutos);

            _produtoRepositorio.ObterProdutoPorId(_produto.Id)
                .Returns(_produto);

            _pedidoRepositorio.ObterPedidos()
                .Returns(pedidos);

            // Act
            var resultado = await _pedidoServico.Adicionar(pedidoComProdutos);

            // Assert
            Assert.False(resultado.HasErrors());
            Assert.Equal(23, pedidoComProdutos.Valor);
            Assert.Equal(20, pedidoComProdutos.ValorTotal);
        }
    }
}
