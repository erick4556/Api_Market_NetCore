using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericSeguridadRepository<T> where T : IdentityUser
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
