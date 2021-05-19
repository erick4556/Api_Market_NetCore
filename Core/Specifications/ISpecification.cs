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
        Expression<Func<T, bool>> Criteria { get; }//Represnta la condición lógica que se va aplicar a una entidad y devuelve un bool

        List<Expression<Func<T,object>>> Includes { get; }//Represnta las relaciones que se va aplicar a la entidad y devuelve un objeto
    }
}
