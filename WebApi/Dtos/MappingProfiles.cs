using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>()//Quiero que la entidad se convierta a una clase Dto
                .ForMember(p => p.CategoriaNombre, x => x.MapFrom(a => a.Categoria.Nombre))//La propiedad CategoriaNombre se va llenar desde la propiedad Categoria que pertenece a Producto. Almacena el nombre en p.CategoriaNombre
                .ForMember(p => p.MarcaNombre, x => x.MapFrom(a => a.Marca.Nombre));

            CreateMap<Direccion, DireccionDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap< DireccionDto, Core.Entities.OrdenCompra.Direccion>();
        }
    }
}
