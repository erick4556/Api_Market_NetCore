﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class OrdenCompras : ClaseBase
    {
        public OrdenCompras()
        {
        }

        public OrdenCompras(string compradorEmail, DateTimeOffset ordenCompraFecha, Direccion direccionEnvio, TipoEnvio tipoEnvio, IReadOnlyList<OrdenItem> orderItems, decimal subTotal)
        {
            CompradorEmail = compradorEmail;
            OrdenCompraFecha = ordenCompraFecha;
            DireccionEnvio = direccionEnvio;
            TipoEnvio = tipoEnvio;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string CompradorEmail { get; set; }

        public DateTimeOffset OrdenCompraFecha { get; set; } = DateTimeOffset.Now;

        public Direccion DireccionEnvio { get; set; }

        public TipoEnvio TipoEnvio {get; set;}

        public IReadOnlyList<OrdenItem> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public OrdenStatus Status { get; set; } = OrdenStatus.Pendiente;
        
        public string PagoIntentoId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + TipoEnvio.Precio;
        }
    }
}
