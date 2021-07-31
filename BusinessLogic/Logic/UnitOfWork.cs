using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;

        private readonly MarketDbContext _context;

        public UnitOfWork(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ClaseBase
        {
           if(_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name; //Para saber la entidad a ubicar que puede ser categoria, producto

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance); //Los parametros son: el índice que representa al item a agregar y el valor a agregar 
            }

            return (IGenericRepository<TEntity>)_repositories[type]; //Retorna una instancia de IGenereicRepository

        }
    }
}
