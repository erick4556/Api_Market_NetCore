using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : ClaseBase //<T> que es generica, otras clases pueden usar las operaciones de esta interfaz
    {
        Task<IReadOnlyList<T>> getAllAsync();
        Task<T> getByIdAsync(int id);
        Task<IReadOnlyList<T>> getAllWithSpec(ISpecification<T> spec);
        Task<T> getByIdWithSpec(ISpecification<T> spec);
        Task<int> countAsync(ISpecification<T> spec);
        Task<int> add(T entity);
        Task<int> update(T entity);
    }
}
