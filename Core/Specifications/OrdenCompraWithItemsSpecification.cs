using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrdenCompraWithItemsSpecification : BaseSpecification<OrdenCompras>
    {
        public OrdenCompraWithItemsSpecification(string email) : base(o=>o.CompradorEmail==email)
        {
            //Las relaciones que va tener
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.TipoEnvio);
            //Orden que va tener
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }

        public OrdenCompraWithItemsSpecification(int id, string email) 
            : base(o => o.CompradorEmail == email && o.Id == id)
        {
            //Las relaciones que va tener
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.TipoEnvio);
            //Orden que va tener
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }

    }
}
