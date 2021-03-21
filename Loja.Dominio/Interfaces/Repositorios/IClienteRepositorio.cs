using Loja.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ObterClientes();
        Task<Cliente> ObterClientePorEmail(string email);
        Task<Cliente> ObterClientePorId(int id);
        void Incluir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(Cliente cliente);
    }
}
