using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //private readonly IProductoRepository _productoRepository;
        private readonly IGenericRepository<Producto> _productoRepository; //Para métodos genericos. La T se convertirá en la clase Producto

        public ProductoController(IGenericRepository<Producto> productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> getProductos()
        {
            //var productos = await _productoRepository.getProductos();
            //var productos = await _productoRepository.getAllAsync(); //Métodos genericos sin la relación
            var spec = new ProductoWithCategoriasAndMarcaSpecification();
            var productos = await _productoRepository.getAllWithSpec(spec);
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> getProductoById(int id){
            //return await _productoRepository.getProductoById(id);
            //return await _productoRepository.getByIdAsync(id);////Métodos genericos sin la relación
            var spec = new ProductoWithCategoriasAndMarcaSpecification(id);
            return await _productoRepository.getByIdWithSpec(spec); //Métodos genericos. spec debe incluir la logica de la condicion de la consulta y tambien las relaciones entre las entidades
        }

    }
}
