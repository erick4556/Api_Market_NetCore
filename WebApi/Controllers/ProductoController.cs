using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    
    public class ProductoController : BaseApiController
    {
        //private readonly IProductoRepository _productoRepository;
        private readonly IGenericRepository<Producto> _productoRepository; //Para métodos genericos. La T se convertirá en la clase Producto
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> getProductos()
        {
            //var productos = await _productoRepository.getProductos();
            //var productos = await _productoRepository.getAllAsync(); //Métodos genericos sin la relación
            var spec = new ProductoWithCategoriasAndMarcaSpecification();
            var productos = await _productoRepository.getAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> getProductoById(int id){
            //return await _productoRepository.getProductoById(id);
            //return await _productoRepository.getByIdAsync(id);////Métodos genericos sin la relación
            var spec = new ProductoWithCategoriasAndMarcaSpecification(id);
            var producto = await _productoRepository.getByIdWithSpec(spec); //Métodos genericos. spec debe incluir la logica de la condicion de la consulta y tambien las relaciones entre las entidades

            if (producto ==  null)
            {
                return NotFound(new CodeErrorResponse(404));
            }

            return _mapper.Map<Producto, ProductoDto>(producto);//Quiero que la entidad se convierta a una clase Dto, el objeto que se va transformar es producto
        }

    }
}
