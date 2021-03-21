using Loja.Dados.Contexto;
using Loja.Dominio.Entidades;
using Loja.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Dados.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private LojaContexto _contexto;

        public PedidoRepositorio(LojaContexto contexto)
        {
            _contexto = contexto;
        }

        public void Atualizar(Pedido pedido)
        {   
            _contexto.Pedidos.Update(pedido);
            _contexto.Entry(pedido).Property(m => m.Numero).IsModified = false;
        }

        public void Excluir(Pedido pedido)
        {
            _contexto.Pedidos.Remove(pedido);
        }

        public void ExcluirItem(PedidoItem pedidoItem)
        {
            _contexto.PedidoItens.Remove(pedidoItem);
        }

        public void Incluir(Pedido pedido)
        {
            _contexto.Pedidos.Add(pedido);
        }

        public async Task<List<PedidoItem>> ObterItensPedidos(int pedidoId)
        {
            return await _contexto.PedidoItens
                .AsNoTracking().Where(m => m.PedidoId == pedidoId)
                .ToListAsync();
        }

        public async Task<Pedido> ObterPedidoPorId(int id)
        {
            return await _contexto.Pedidos
                .Include(m=>m.Cliente)
                .Include(m=>m.Produtos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Pedido>> ObterPedidos()
        {
            return await _contexto.Pedidos.AsNoTracking()
                .ToListAsync();
        }
    }
}
