using Loja.Dados.Contexto;
using Loja.Dominio.Interfaces;
using System.Threading.Tasks;

namespace Loja.Dados
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LojaContexto _contexto;

        public UnitOfWork(LojaContexto contexto) =>
            _contexto = contexto;

        public async Task<bool> Commit()
        {
            var success = (await _contexto.SaveChangesAsync()) > 0;
            return success;
        }

        public void Dispose() =>
            _contexto.Dispose();

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
