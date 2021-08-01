using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class OrdenCompraResponseDto
    {
        public int Id { get; set; }
        public string CompradorEmail { get; set; }

        public DateTimeOffset OrdenCompraFecha { get; set; }

        public Direccion DireccionEnvio { get; set; }

        public string TipoEnvio { get; set; }

        public decimal TipoEnvioPrecio { get; set; }

        public IReadOnlyList<OrdenItemResponseDto> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }
    }
}
