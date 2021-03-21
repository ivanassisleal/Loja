using System;
using System.Threading.Tasks;

namespace Loja.Dominio.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
