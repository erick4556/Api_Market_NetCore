using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
   public interface IUnitOfWork : IDisposable //IDisposable para que al realizar la transacción la instancia de IUnitOfWork tambien se elimine. Disposable significa que puede ser eliminado
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ClaseBase;

        Task<int> Complete();

    }
}
