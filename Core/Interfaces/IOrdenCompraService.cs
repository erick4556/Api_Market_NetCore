using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdenCompraService
    {
        Task<OrdenCompras> addOrdenCompra(string compradorEmail, int tipoEnvio, string productoId, Direccion direccion);

        Task<IReadOnlyList<OrdenCompras>>getOrdenComprasByUserEmail(string email);

        Task<OrdenCompras> getOrdenComprasById(int id, string email);

        Task<IReadOnlyList<TipoEnvio>> getTipoEnvios();
    }
}
