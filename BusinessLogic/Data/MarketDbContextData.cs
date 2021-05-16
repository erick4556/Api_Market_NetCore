using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
   public class MarketDbContextData
    {
        public static async Task CargarDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Marca.Any()) //Any indica si tiene algun valor
                {
                    var marcaData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData); //Le de un formato tipo list, una colección de la clase marca 
                    foreach(var marca in marcas)
                    {
                       context.Marca.Add(marca);
                    }

                    await context.SaveChangesAsync();//Insertar la data en la tabla
                }

                if (!context.Categoria.Any()) //Any indica si tiene algun valor
                {
                    var categoriaData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData); //Le de un formato tipo list, una colección de la clase categoria 
                    foreach (var categoria in categorias)
                    {
                        context.Categoria.Add(categoria);
                    }

                    await context.SaveChangesAsync();//Insertar la data en la tabla
                }

                if (!context.Producto.Any()) //Any indica si tiene algun valor
                {
                    var productoData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productoData); //Le de un formato tipo list, una colección de la clase producto 
                    foreach (var producto in productos)
                    {
                        context.Producto.Add(producto);
                    }

                    await context.SaveChangesAsync();//Insertar la data en la tabla
                }


            }
            catch(Exception e)
            {
                var logger = loggerFactory.CreateLogger<MarketDbContextData>(); //Logger se aplique sobre la clase actual, MarketDbContextData
                logger.LogError(e.Message);
            }
        }
    }
}
