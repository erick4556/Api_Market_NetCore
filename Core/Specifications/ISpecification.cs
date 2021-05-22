using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }//Representa la condición lógica que se va aplicar a una entidad y devuelve un bool

        List<Expression<Func<T,object>>> Includes { get; }//Representa las relaciones que se va aplicar a la entidad y devuelve un objeto

        Expression<Func<T,object>> OrderBy { get; } //Devuelve un objeto = object

        Expression<Func<T,object>> OrderByDescending { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }

    }
}
