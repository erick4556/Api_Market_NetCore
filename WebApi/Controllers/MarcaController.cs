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
    
    public class MarcaController : BaseApiController
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
