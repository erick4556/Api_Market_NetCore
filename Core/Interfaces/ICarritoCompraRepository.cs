using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICarritoCompraRepository
    {
        Task<CarritoCompra> getCarritoCompra(string carritoId);

        Task<CarritoCompra> updateCarritoCompra(CarritoCompra carritoCompra);

        Task<bool> deleteCarritoCompra(string carritoId);
    }
}
