using AutoMapper;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{

    [Authorize]
    public class OrdenCompraController : BaseApiController
    {
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly IMapper _mapper;

        public OrdenCompraController(IOrdenCompraService ordenCompraService, IMapper mapper)
        {
            _ordenCompraService = ordenCompraService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<OrdenCompraResponseDto>> addOrdenCompra(OrdenCompraDto ordeCompraDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value; //Se obtiene este email desde el token que envía el usuario
            var direccion = _mapper.Map<DireccionDto, Direccion>(ordeCompraDto.DireccionEnvio);
            var ordenCompra = await _ordenCompraService.addOrdenCompra(email, ordeCompraDto.TipoEnvio, ordeCompraDto.CarritoCompraId, direccion);

            if (ordenCompra == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Errores creando la orden de compra"));
            }
            else
            {
                return Ok(_mapper.Map<OrdenCompras, OrdenCompraResponseDto>(ordenCompra));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrdenCompraResponseDto>>> getOrdenCompras()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value; //Se obtiene este email desde el token que envía el usuario
            var ordenCompras = await _ordenCompraService.getOrdenComprasByUserEmail(email);
            return Ok(_mapper.Map<IReadOnlyList<OrdenCompras>, IReadOnlyList<OrdenCompraResponseDto>>(ordenCompras)); //Se le pasa el origen, el destino y la data que se va a transformar
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompraResponseDto>> getOrdenComprasById(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value; //Se obtiene este email desde el token que envía el usuario
            var ordenCompra = await _ordenCompraService.getOrdenComprasById(id, email);
            if (ordenCompra == null)
            {
                return NotFound(new CodeErrorResponse(404, "No se encontró la orden de compra"));
            }
            else
            {
                return _mapper.Map<OrdenCompras, OrdenCompraResponseDto>(ordenCompra);
            }
        }

        [HttpGet("tipoEnvio")]
        public async Task<ActionResult<IReadOnlyList<TipoEnvio>>> getTipoEnvio()
        {
            return Ok(await _ordenCompraService.getTipoEnvios());
        }

    }
}
