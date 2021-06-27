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
    
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoCompra;

        public CarritoCompraController(ICarritoCompraRepository carritoCompra)
        {
            _carritoCompra = carritoCompra;
        }

        [HttpGet]
        public async Task<ActionResult<CarritoCompra>> getCarritoCompraById(string id)
        {
          var carrito = await _carritoCompra.getCarritoCompra(id);

            return Ok(carrito ?? new CarritoCompra(id));//Si es null devuelva un objeto de CarritoCompra
        }

        [HttpPost]
        public async Task<ActionResult<CarritoCompra>> updateCarritoCompra(CarritoCompra carritoParam)
        {
         var carritoUpdated = await  _carritoCompra.updateCarritoCompra(carritoParam);

            return Ok(carritoUpdated);
        }

        [HttpDelete]
        public async Task deleteCarritoCompra(string id)
        {
           await _carritoCompra.deleteCarritoCompra(id);
        }

    }
}
