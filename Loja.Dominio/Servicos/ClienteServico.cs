using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using System;
using System.Threading.Tasks;

namespace Loja.Dominio.Servicos
{
    public class ClienteServico : ServicoBase, IClienteServico
    {
        private IClienteRepositorio _clienteRepositorio;
        private IUnitOfWork _unitOfWork;

        public ClienteServico(
            IClienteRepositorio clienteRepositorio,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<Resultado> Adicionar(Cliente cliente)
        {
            var resultado = new Resultado();

            cliente.Validar();
            if (!cliente.IsValid) AddNotifications(cliente.Notifications);

            if(!await EmailDisponivel(cliente.Email))
                AddNotification("Email", "E-mail informado indisponível");

            if (IsValid)
            {
                _clienteRepositorio.Incluir(cliente);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Atualizar(Cliente cliente)
        {
            var resultado = new Resultado();

            cliente.Validar();
            if (!cliente.IsValid) AddNotifications(cliente.Notifications);

            var clientePersistido = await _clienteRepositorio
                .ObterClientePorId(cliente.Id);

            if (clientePersistido != null)
            {
                if (clientePersistido.Email != cliente.Email)
                {
                    if (!await EmailDisponivel(cliente.Email))
                        AddNotification("Email", "E-mail informado indisponível");
                }
            } else AddNotification("Id", "Cliente não encontrado");

            if (IsValid)
            {
                _clienteRepositorio.Atualizar(cliente);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Excluir(int id)
        {
            var resultado = new Resultado();

            var cliente = await _clienteRepositorio.ObterClientePorId(id);
            if (cliente == null)
                AddNotification("Id", "Cliente não encontrado");

            if (IsValid)
            {
                _clienteRepositorio.Excluir(cliente);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        private async Task<bool> EmailDisponivel(string email)
        {
            var cliente = await _clienteRepositorio
               .ObterClientePorEmail(email);

            return cliente == null;
        }
    }
}
