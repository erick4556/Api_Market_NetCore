using AutoMapper;
using Core.Entities;
using Core.Entities.OrdenCompra;
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

            CreateMap<Core.Entities.Direccion, DireccionDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<DireccionDto, Core.Entities.OrdenCompra.Direccion>();
            CreateMap<OrdenCompras, OrdenCompraResponseDto>()
                .ForMember(o => o.TipoEnvio, x => x.MapFrom(y => y.TipoEnvio.Nombre)) //OrdenCompras es el origen y OrdenCompraResponseDto el destino. Indica que la propiedad TipoEnvio va llenarse desde la propiedad desde el valor de la entidad TipoEnvio en su propiedad Nombre
                .ForMember(o => o.TipoEnvioPrecio, x => x.MapFrom(y => y.TipoEnvio.Precio));

            CreateMap<OrdenItem, OrdenItemResponseDto>()
                .ForMember(o => o.ProductoId, x => x.MapFrom(y => y.ItemOrdenado.ProductoItemId))
                .ForMember(o => o.ProductoNombre, x => x.MapFrom(y => y.ItemOrdenado.ProductoNombre))
                .ForMember(o => o.ProductoImagen, x => x.MapFrom(y => y.ItemOrdenado.ImagenUrl));
        }
    }
}
