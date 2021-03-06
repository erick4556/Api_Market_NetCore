using BusinessLogic.Data;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericSeguridadRepository<T> : IGenericSeguridadRepository<T> where T : IdentityUser
    {

        private readonly SeguridadDbContext _context;

        public GenericSeguridadRepository(SeguridadDbContext context)
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
            return SeguridadSpecificationEvaluator<T>.getQuery(_context.Set<T>().AsQueryable(), spec);
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
    }
}
