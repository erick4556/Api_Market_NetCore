using Core.Entities;
using Core.Entities.OrdenCompra;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OrdenCompraService : IOrdenCompraService
    {

        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrdenCompraService(ICarritoCompraRepository carritoCompraRepository, IUnitOfWork unitOfWork)
        {
            _carritoCompraRepository = carritoCompraRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrdenCompras> addOrdenCompra(string compradorEmail, int tipoEnvio, string carritoId, Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.getCarritoCompra(carritoId);

            var items = new List<OrdenItem>();

            foreach (var item in carritoCompra.Items)
            {
                var productoItem = await _unitOfWork.Repository<Producto>().getByIdAsync(item.Id);
                var itemOrdenado = new ProductoItemOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Image);
                var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);
                items.Add(ordenItem);
            }

            var tipoEnvioEntity = await _unitOfWork.Repository<TipoEnvio>().getByIdAsync(tipoEnvio);

            var subTotal = items.Sum(item => item.Precio * item.Cantidad);

            var ordenCompra = new OrdenCompras(compradorEmail, direccion, tipoEnvioEntity, items, subTotal);

            _unitOfWork.Repository<OrdenCompras>().addEntity(ordenCompra);

            var resultado = await _unitOfWork.Complete();

            if (resultado <= 0)
            {
                return null;
            }
            else
            {
                await _carritoCompraRepository.deleteCarritoCompra(carritoId);
                return ordenCompra;
            }


        }

        public async Task<OrdenCompras> getOrdenComprasById(int id, string email)
        {
            var specification = new OrdenCompraWithItemsSpecification(id, email);

            return await _unitOfWork.Repository<OrdenCompras>().getByIdWithSpec(specification);
        }

        public async Task<IReadOnlyList<OrdenCompras>> getOrdenComprasByUserEmail(string email)
        {
            var specification = new OrdenCompraWithItemsSpecification(email);

            return await _unitOfWork.Repository<OrdenCompras>().getAllWithSpec(specification);

        }

        public async Task<IReadOnlyList<TipoEnvio>> getTipoEnvios()
        {
            return await _unitOfWork.Repository<TipoEnvio>().getAllAsync();
        }
    }
}
