using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Dominio.Servicos
{
    public class PedidoServico : ServicoBase, IPedidoServico
    {
        private IPedidoRepositorio _pedidoRepositorio;
        private IProdutoRepositorio _produtoRepositorio;
        private IUnitOfWork _unitOfWork;

        public PedidoServico(
            IProdutoRepositorio produtoRepositorio,
            IPedidoRepositorio pedidoRepositorio,
            IUnitOfWork unitOfWork)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<Resultado> Adicionar(Pedido pedido)
        {
            var resultado = new Resultado();

            if (pedido.Produtos.Count == 0)
            {
                AddNotification("", "Pedido precisa ter pelo menos um produto");
                resultado.AddErrors(Notifications);
                return resultado;
            };

            decimal valorPedidoSemDesconto = await CalcularValorDoPedidoSemDesconto(pedido);
            if (!IsValid)
            {
                resultado.AddErrors(Notifications);
                return resultado;
            }

            decimal valorTotalDoPedido = CalcularValorDoPedido(valorPedidoSemDesconto,pedido.Desconto);
            if (!IsValid)
            {
                resultado.AddErrors(Notifications);
                return resultado;
            }

            pedido.Valor = valorPedidoSemDesconto;
            pedido.ValorTotal = valorTotalDoPedido;

            pedido.Validar();
            if (!pedido.IsValid) AddNotifications(pedido.Notifications);

            pedido.Numero = await ObterNumeroPedido();

            if (IsValid)
            {
                _pedidoRepositorio.Incluir(pedido);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Atualizar(Pedido pedido)
        {
            var resultado = new Resultado();

            if (pedido.Produtos.Count == 0)
            {
                AddNotification("", "Pedido precisa ter pelo menos um produto");
                resultado.AddErrors(Notifications);
                return resultado;
            };

            decimal valorPedidoSemDesconto = await CalcularValorDoPedidoSemDesconto(pedido);
            if (!IsValid)
            {
                resultado.AddErrors(Notifications);
                return resultado;
            }

            decimal valorTotalDoPedido = CalcularValorDoPedido(valorPedidoSemDesconto, pedido.Desconto);
            if (!IsValid)
            {
                resultado.AddErrors(Notifications);
                return resultado;
            }

            pedido.Valor = valorPedidoSemDesconto;
            pedido.ValorTotal = valorTotalDoPedido;

            pedido.Validar();
            if (!pedido.IsValid) AddNotifications(pedido.Notifications);

            if (IsValid)
            {
                var pedidoPersistido = await _pedidoRepositorio.ObterItensPedidos(pedido.Id);
                foreach (var produtoPedido in pedidoPersistido)
                    _pedidoRepositorio.ExcluirItem(produtoPedido);

                _pedidoRepositorio.Atualizar(pedido);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Excluir(int id)
        {
            var resultado = new Resultado();

            var pedido = await _pedidoRepositorio.ObterPedidoPorId(id);
            if (pedido == null)
                AddNotification("Id", "Pedido não encontrado");

            if (IsValid)
            {
                _pedidoRepositorio.Excluir(pedido);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        private async Task<int> ObterNumeroPedido()
        {
            var pedidos = await _pedidoRepositorio.ObterPedidos();

            if (pedidos.Count == 0)
                return 1;

            return pedidos.Max(m => m.Numero) + 1;
        }

        private async Task<decimal> CalcularValorDoPedidoSemDesconto(Pedido pedido)
        {
            decimal valorTotalDosProdutos = 0;

            foreach (var pedidoProduto in pedido.Produtos)
            {
                var produto = await _produtoRepositorio
                    .ObterProdutoPorId(pedidoProduto.ProdutoId);

                if (produto == null)
                {
                    AddNotification("", "Produto Informado no pedido não encontrado");
                    return 0;
                } else valorTotalDosProdutos += produto.Valor;
            }

            return valorTotalDosProdutos;
        }

        private decimal CalcularValorDoPedido(decimal valorTotalDosProdutos, decimal desconto)
        {
            decimal valorTotalDoPedido = 0;

            if (desconto > 0)
            {
                valorTotalDoPedido = valorTotalDosProdutos - desconto;

                if (valorTotalDoPedido <= 0)
                {
                    AddNotification("", "Valor Total do Pedido não pode ser Zero");
                    return 0;
                }
            }

            return valorTotalDoPedido;
        }
    }
}
