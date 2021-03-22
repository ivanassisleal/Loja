using Loja.Dados.Repositorios;
using Loja.Dominio.Entidades;
using System.Threading.Tasks;
using Xunit;

namespace Loja.Dados.Testes.Repositorios
{
    public class ClienteRepositorioTestes
    {
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly UnitOfWork _unitOfWork;

        public ClienteRepositorioTestes()
        {
            var bd = new BdMemoria();
            var contexto = bd.ObterContexto();
            _clienteRepositorio = new ClienteRepositorio(contexto);
            _unitOfWork = new UnitOfWork(contexto);
        }

        [Fact]
        public async Task ObterClientes()
        {
            // Act
            var clientes = await _clienteRepositorio.ObterClientes();

            // Assert
            Assert.Equal(3, clientes.Count);
        }

        [Fact]
        public async Task ObterClientePorId()
        {
            // Act
            var cliente = await _clienteRepositorio.ObterClientePorId(1);

            // Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public async Task ObterClientePorEmail()
        {
            // Act
            var cliente = await _clienteRepositorio.ObterClientePorEmail("sicrano@gmail.com");

            // Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public async Task IncluirCliente()
        {
            // Arrange
             _clienteRepositorio.Incluir(new Cliente()
            {
                Nome = "Maria",
                Email = "maria@gmail.com",
                Aldeia = "A4"
            });

            await _unitOfWork.Commit();

            // Act
            var cliente = await _clienteRepositorio.ObterClientePorEmail("maria@gmail.com");

            // Assert
            Assert.NotNull(cliente);
        }
    }
}
