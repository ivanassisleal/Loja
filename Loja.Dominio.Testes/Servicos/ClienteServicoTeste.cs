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
    public class ClienteServicoTeste
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IClienteServico _clienteServico;
        private Cliente _cliente;

        public ClienteServicoTeste()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _clienteRepositorio = Substitute.For<IClienteRepositorio>();
            _clienteServico = new ClienteServico(_clienteRepositorio, _unitOfWork);
            _cliente = new Cliente() { Id = 1, Nome = "Fulano", Email = "fulano@gmail.com", Aldeia = "A1" };
        }

        [Fact]
        public async Task CriarClienteComEmailEnexistenteRetornoComSucesso()
        {
            Cliente clienteExistente = null;

            _clienteRepositorio.ObterClientePorEmail(_cliente.Email)
                .Returns(clienteExistente);

            _clienteRepositorio.Incluir(_cliente);

            // Act
            var resultado = await _clienteServico.Adicionar(_cliente);

            // Assert
            Assert.False(resultado.HasErrors());

        }

        [Fact]
        public async Task CriarClientComEmailExistenteRetornoComFalha()
        {
            // Arrange
            Cliente clienteExistente = null;

            Cliente clienteNomeInvalido = new Cliente() { 
                Nome = "",
                Email = "fulano@gmail.com", 
                Aldeia = "A1"
            };

            _clienteRepositorio.ObterClientePorEmail(clienteNomeInvalido.Email)
                .Returns(clienteExistente);

            // Act
            var resultado = await _clienteServico.Adicionar(clienteNomeInvalido);

            // Assert
            Assert.True(resultado.HasErrors());

        }

        [Fact]
        public async Task CriarClienteSemNomeRetornoComFalha()
        {
            _clienteRepositorio.ObterClientePorEmail(_cliente.Email)
                .Returns(_cliente);

            // Act
            var resultado = await _clienteServico.Adicionar(_cliente);

            // Assert
            Assert.True(resultado.HasErrors());

        }

        [Fact]
        public async Task ExcluirClienteNaoEncontradoRetornoComFalha()
        {
            // Arrange
            Cliente clienteExistente = null;

            _clienteRepositorio.ObterClientePorId(_cliente.Id)
               .Returns(clienteExistente);

            // Act
            var resultado = await _clienteServico.Excluir(_cliente.Id);

            // Assert
            Assert.True(resultado.HasErrors());

        }

        [Fact]
        public async Task ExcluirClienteEncontradoRetornoComSucesso()
        {
            // Arrange
           
            _clienteRepositorio.ObterClientePorId(_cliente.Id)
               .Returns(_cliente);

            _clienteRepositorio.Excluir(_cliente);

            // Act
            var resultado = await _clienteServico.Excluir(_cliente.Id);

            // Assert
            Assert.False(resultado.HasErrors());

        }
    }
}
