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
        public ProductoWithCategoriasAndMarcaSpecification(ProductoSpecificationParams productoParams) :
            base(x =>
                    (string.IsNullOrEmpty(productoParams.Search) || x.Nombre.Contains(productoParams.Search)) && //Validar que el valor no sea null sino compara con la propiedad nombre el parámetro search
                    (!productoParams.Marca.HasValue || x.MarcaId == productoParams.Marca) && //base() para llamar al constructor de la clase padre
                    (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
            )
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);

            //ApplyPaging(0, 5);//De la posición 0 trae 5 registros

            ApplyPaging(productoParams.PageSize * (productoParams.PageIndex - 1), productoParams.PageSize);

            if (!string.IsNullOrEmpty(productoParams.Sort))
            {
                switch (productoParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre);
                        break;
                    case "precioAsc":
                        AddOrderBy(p => p.Precio);
                        break;
                    case "precioDesc":
                        AddOrderByDescending(p => p.Precio);
                        break;
                    case "descripcionAsc":
                        AddOrderBy(p => p.Descripcion);
                        break;
                    case "descripcionDesc":
                        AddOrderByDescending(p => p.Descripcion);
                        break;
                    default:
                        AddOrderBy(p => p.Nombre);
                        break;
                }
            }
        }

        public ProductoWithCategoriasAndMarcaSpecification(int id) : base(x => x.Id == id) //Podria agregar busqueda por nombre o otra propiedad modificando base()
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }

    }
}
