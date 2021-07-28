using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OrdenCompraService : IOrdenCompraService
    {

        private readonly IGenericRepository<OrdenCompras> _ordenComprasRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IGenericRepository<TipoEnvio> _tipoEnvioRepository;

        public OrdenCompraService(IGenericRepository<OrdenCompras> ordenComprasRepository, IGenericRepository<Producto> productoRepository, ICarritoCompraRepository carritoCompraRepository, IGenericRepository<TipoEnvio> tipoEnvioRepository)
        {
            _ordenComprasRepository = ordenComprasRepository;
            _productoRepository = productoRepository;
            _carritoCompraRepository = carritoCompraRepository;
            _tipoEnvioRepository = tipoEnvioRepository;
        }

        public async Task<OrdenCompras> addOrdenCompra(string compradorEmail, int tipoEnvio, string carritoId, Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.getCarritoCompra(carritoId);

            var items = new List<OrdenItem>();

            foreach (var item in carritoCompra.Items)
            {
                var productoItem = await _productoRepository.getByIdAsync(item.Id);
                var itemOrdenado = new ProductoItemOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Image);
                var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);
                items.Add(ordenItem);
            }

            var tipoEnvioEntity = await _tipoEnvioRepository.getByIdAsync(tipoEnvio);

            var subTotal = items.Sum(item => item.Precio * item.Cantidad);

            var ordenCompra = new OrdenCompras(compradorEmail, direccion, tipoEnvioEntity, items, subTotal);

            return ordenCompra;

        }

        public Task<OrdenCompras> getOrdenComprasById(int id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrdenCompras>> getOrdenComprasByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TipoEnvio>> getTipoEnvios()
        {
            throw new NotImplementedException();
        }
    }
}
