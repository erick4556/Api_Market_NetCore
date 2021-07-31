using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClaseBase
    {

        private readonly MarketDbContext _context;

        public GenericRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> getAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> getAllWithSpec(ISpecification<T> spec)
        {
            return await applySpecification(spec).ToListAsync();
        }

        public async Task<T> getByIdWithSpec(ISpecification<T> spec)
        {
            return await applySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> applySpecification(ISpecification<T> spec)
        {
           return SpecificationEvaluator<T>.getQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> countAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).CountAsync();
        }

        public async Task<int> add(T entity)
        {
            _context.Set<T>().Add(entity);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified; //Actualice los valores y no los duplique en la bd
            return await _context.SaveChangesAsync();
        }

        //Los cambios para agregar un nuevo elemento van estar en memoria hasta que se dispare el context UnitOfWork
        public void addEntity(T Entity)
        {   
            
            _context.Set<T>().Attach(Entity);
            //El SaveChangesAsync(); lo va hacer el UnitOfWork
        }

        public void updateEntity(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified; //Actualice los valores y no los duplique en la bd
        }

        public void deleteEntity(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
    }
}
