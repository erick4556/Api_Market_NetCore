using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class RegistrarDto
    {
        public String Email { get; set; }

        public String Username { get; set; }

        public String Nombre { get; set; }

        public String Apellido { get; set; }

        public String Password { get; set; }
    }
}
