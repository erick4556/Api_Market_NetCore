using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
   public class ProductoWithCategoriasAndMarcaSpecification : BaseSpecification<Producto>
    {
        public ProductoWithCategoriasAndMarcaSpecification()
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }

        public ProductoWithCategoriasAndMarcaSpecification(int id) : base(x=> x.Id == id) //Podria agregar busqueda por nombre o otra propiedad modificando base()
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }

    }
}
