using Core.Entities;
using Core.Interfaces;
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
            //var productos = await _productoRepository.getProducto();
            var productos = await _productoRepository.getAllAsync(); //Métodos genericos
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> getProductoById(int id){
            //return await _productoRepository.getProductoById(id);
            return await _productoRepository.getByIdAsync(id); //Métodos genericos
        }

    }
}
