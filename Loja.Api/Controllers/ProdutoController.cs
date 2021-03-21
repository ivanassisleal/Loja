using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Api.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        IProdutoServico _produtoServico;
        IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(
            IProdutoRepositorio produtoRepositorio,
            IProdutoServico produtoServico)
        {
            _produtoRepositorio = produtoRepositorio;
            _produtoServico = produtoServico;
        }


        [HttpGet]
        public async Task<IActionResult> ObterProdutos()
        {
            var produtos = await _produtoRepositorio.ObterProdutos();
            if (produtos == null)
                return NoContent();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProduto([FromRoute] int id)
        {
            var produto = await _produtoRepositorio.ObterProdutoPorId(id);
            if (produto == null)
                return NoContent();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> IncluirProduto([FromBody] Produto produto)
        {
            var resultado = await _produtoServico.Adicionar(produto);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return CreatedAtAction( nameof(IncluirProduto), new { id = produto.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaProduto([FromRoute] long id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            var resultado = await _produtoServico.Atualizar(produto);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto([FromRoute] int id)
        {
            var resultado = await _produtoServico.Excluir(id);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }
    }
}
