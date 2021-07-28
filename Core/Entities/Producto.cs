using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Producto : ClaseBase //La clase Producto se herede desde ClaseBase
    {
       
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int MarcaId { get; set; } //Entity framework tomara MarcaId como clave foranea

        public Marca Marca { get; set; } //Entity framework y tomara Marca como referencia

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public decimal Precio { get; set; }

        public string Image { get; set; }
    }
}
