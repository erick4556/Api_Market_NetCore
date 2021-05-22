using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductoForCountingSpecification : BaseSpecification<Producto>
    {

        //Solo se requiere la cantidad de elemento que tiene por producto
        public ProductoForCountingSpecification(ProductoSpecificationParams productoParams) :
            base(x => (!productoParams.Marca.HasValue || x.MarcaId == productoParams.Marca) && //base() para llamar al constructor de la clase padre
                    (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
            )
        {

        }
    }
}
