using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {

        public static async Task SeedUser(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any()){ //Si no tiene ningún registro
                var usuario = new Usuario
                {
                    Nombre = "Carl",
                    Apellido = "Mendez",
                    UserName = "carlmendez",
                    Email = "carlmendes@test.com",
                    Direccion = new Direccion
                    {
                        Calle = "Las Colinas 019",
                        Ciudad = "Chile",
                        CodigoPostal = "29101",
                        Departamento = "Chile"
                    }
                };

               await userManager.CreateAsync(usuario, "Password123!");

            }
        }

    }
}
