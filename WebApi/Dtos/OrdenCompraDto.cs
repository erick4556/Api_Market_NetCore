using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class OrdenCompraDto
    {
        public string CarritoCompraId { get; set; }

        public int TipoEnvio { get; set; }

        public DireccionDto DireccionEnvio { get; set; }

    }
}
