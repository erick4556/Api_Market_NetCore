using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UsuarioSpecification : BaseSpecification<Usuario>
    {
        public UsuarioSpecification(UsuarioSpecificationParams usuarioParams) : base(x =>
        (string.IsNullOrEmpty(usuarioParams.Search) || x.Nombre.Contains(usuarioParams.Search)) && //Para saber si se esta enviando un valor en blanco o no
        (string.IsNullOrEmpty(usuarioParams.Nombre) || x.Nombre.Contains(usuarioParams.Nombre)) &&
        (string.IsNullOrEmpty(usuarioParams.Apellido) || x.Nombre.Contains(usuarioParams.Apellido)))
        {
            ApplyPaging(usuarioParams.PageSize * (usuarioParams.PageIndex - 1), usuarioParams.PageSize);

            if (!string.IsNullOrEmpty(usuarioParams.Sort))
            {
                switch (usuarioParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(u => u.Nombre);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(u => u.Nombre);
                        break;

                    case "emaileAsc":
                        AddOrderBy(u => u.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(u => u.Email);
                        break;

                    default:
                        AddOrderBy(u => u.Nombre);
                        break;
                }

            }
        }
    }
}
