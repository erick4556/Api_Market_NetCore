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
    public class MarcaController : ControllerBase
    {
        private readonly IGenericRepository<Marca> _marcaRepository;

        public MarcaController(IGenericRepository<Marca> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Marca>>> getMarcas()
        {
            var marcas = await _marcaRepository.getAllAsync();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> getMarcaById(int id)
        {
            return await _marcaRepository.getByIdAsync(id);
        }

    }
}
