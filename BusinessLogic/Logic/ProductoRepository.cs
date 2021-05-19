using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly MarketDbContext _context;
        public ProductoRepository(MarketDbContext context)
        {
            _context = context;

        }
        public async Task<IReadOnlyList<Producto>> getProductos()
        {
            //return await _context.Producto.ToListAsync();
            return await _context.Producto.Include(p => p.Marca).Include(p => p.Categoria).ToListAsync();//Consulta con la relación
        }

        public async Task<Producto> getProductoById(int id)
        {
            //return await _context.Producto.FindAsync(id);
            return await _context.Producto.Include(p => p.Marca).Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);//Consulta con la relación
        }
    }
}
