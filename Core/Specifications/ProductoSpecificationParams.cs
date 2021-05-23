using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductoSpecificationParams
    {
        public int? Marca { get; set; } //int? significa que es un valor opcional o que puede que sea nulo o tenga un valor

        public int? Categoria { get; set; }

        public string Sort { get; set; }

        public int PageIndex { get; set; } = 1; //Por defecto el valor va ser 1

        private const int MaxPageSize = 50; //50 registros máximo por página

        private int _pageSize = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Search { get; set; }

    }
}
