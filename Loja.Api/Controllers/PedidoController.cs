using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Loja.Api.Controllers
{
    [Route("pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        IPedidoServico _pedidoServico;
        IPedidoRepositorio _pedidoRepositorio;

        public PedidoController(
            IPedidoRepositorio pedidoRepositorio,
            IPedidoServico pedidoServico)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _pedidoServico = pedidoServico;
        }


        [HttpGet]
        public async Task<IActionResult> ObterPedidos()
        {
            var pedidos = await _pedidoRepositorio.ObterPedidos();
            if (pedidos == null)
                return NoContent();

            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedido([FromRoute] int id)
        {
            var pedido = await _pedidoRepositorio.ObterPedidoPorId(id);
            if (pedido == null)
                return NoContent();

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> IncluirPedido([FromBody] PedidoViewModel pedidoViewModel)
        {
            var pedido = ConverterViewModelParaEntidade(pedidoViewModel);

            var resultado = await _pedidoServico.Adicionar(pedido);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return CreatedAtAction( nameof(IncluirPedido), new { id = pedido.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido([FromRoute] long id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id) return BadRequest();

            var resultado = await _pedidoServico.Atualizar(pedido);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPedido([FromRoute] int id)
        {
            var resultado = await _pedidoServico.Excluir(id);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }

        private Pedido ConverterViewModelParaEntidade(PedidoViewModel pedidoViewModel)
        {
            var pedido = new Pedido()
            {
                Id = pedidoViewModel.Id,
                ClienteId = pedidoViewModel.ClienteId,
                Desconto = pedidoViewModel.Desconto
            };

            foreach (var produto in pedidoViewModel.Produtos)
            {
                pedido.Produtos.Add(new PedidoItem()
                {
                    Id =  Guid.NewGuid(),
                    ProdutoId = produto.ProdutoId
                });
            }

            return pedido;
        }
    }
}
