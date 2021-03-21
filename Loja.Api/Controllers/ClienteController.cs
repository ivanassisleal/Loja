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
    [Route("clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        IClienteServico _clienteServico;
        IClienteRepositorio _clienteRepositorio;

        public ClienteController(
            IClienteRepositorio clienteRepositorio,
            IClienteServico clienteServico)
        {
            _clienteRepositorio = clienteRepositorio;
            _clienteServico = clienteServico;
        }


        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _clienteRepositorio.ObterClientes();
            if (clientes == null)
                return NoContent();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCliente([FromRoute] int id)
        {
            var cliente = await _clienteRepositorio.ObterClientePorId(id);
            if (cliente == null)
                return NoContent();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> IncluirCliente([FromBody] Cliente cliente)
        {
            var resultado = await _clienteServico.Adicionar(cliente);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return CreatedAtAction( nameof(IncluirCliente), new { id = cliente.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] long id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();

            var resultado = await _clienteServico.Atualizar(cliente);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCliente([FromRoute] int id)
        {
            var resultado = await _clienteServico.Excluir(id);
            if (resultado.HasErrors())
                return BadRequest(resultado.GetErrors());

            return NoContent();
        }
    }
}
