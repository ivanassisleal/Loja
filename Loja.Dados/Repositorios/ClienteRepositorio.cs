using Loja.Dados.Contexto;
using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Dados.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private LojaContexto _contexto;

        public ClienteRepositorio(LojaContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
        }

        public void Excluir(Cliente cliente)
        {
            _contexto.Clientes.Remove(cliente);
        }

        public void Incluir(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
        }

        public async Task<List<Cliente>> ObterClientes()
        {
            return await _contexto.Clientes.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cliente> ObterClientePorEmail(string email)
        {
            return await _contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Cliente> ObterClientePorId(int id)
        {
            return await _contexto.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
