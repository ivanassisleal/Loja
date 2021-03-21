using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using System;
using System.Threading.Tasks;

namespace Loja.Dominio.Servicos
{
    public class ProdutoServico : ServicoBase, IProdutoServico
    {
        private IProdutoRepositorio _produtoRepositorio;
        private IUnitOfWork _unitOfWork;

        public ProdutoServico(
            IProdutoRepositorio produtoRepositorio,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<Resultado> Adicionar(Produto produto)
        {
            var resultado = new Resultado();

            produto.Validar();
            if (!produto.IsValid) AddNotifications(produto.Notifications);

            if (IsValid)
            {
                _produtoRepositorio.Incluir(produto);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Atualizar(Produto produto)
        {
            var resultado = new Resultado();

            produto.Validar();
            if (!produto.IsValid) AddNotifications(produto.Notifications);

            if (IsValid)
            {
                _produtoRepositorio.Atualizar(produto);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }

        public async Task<Resultado> Excluir(int id)
        {
            var resultado = new Resultado();

            var cliente = await _produtoRepositorio.ObterProdutoPorId(id);
            if (cliente == null)
                AddNotification("Id", "Produto não encontrado");

            if (IsValid)
            {
                _produtoRepositorio.Excluir(cliente);
                await _unitOfWork.Commit();
            }
            else
                resultado.AddErrors(Notifications);

            return resultado;
        }
    }
}
