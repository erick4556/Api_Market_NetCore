using Core.Entities;
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
    }
}
