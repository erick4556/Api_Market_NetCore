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
    }
}
