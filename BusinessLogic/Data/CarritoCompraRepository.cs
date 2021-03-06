using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class CarritoCompraRepository : ICarritoCompraRepository
    {

        private readonly IDatabase _database;

        public CarritoCompraRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CarritoCompra> getCarritoCompra(string carritoId)
        {
          var data = await _database.StringGetAsync(carritoId);

          return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CarritoCompra>(data); //Deserealiza el objeto CarritoCompra
        }

        public async Task<CarritoCompra> updateCarritoCompra(CarritoCompra carritoCompra)
        {
            //Convierto a string el objeto carritoCompra. Si no existe el id crea un nuevo record y si existe lo actualiza. Va estar disponible la data por 30 días
          var status = await  _database.StringSetAsync(carritoCompra.Id, JsonSerializer.Serialize(carritoCompra), TimeSpan.FromDays(30));

            if (!status)
            {
                return null;
            }
            else
            {
                return await getCarritoCompra(carritoCompra.Id);
            }
        }
        public async Task<bool> deleteCarritoCompra(string carritoId)
        {
            return await _database.KeyDeleteAsync(carritoId); //Devuelve un valor bool
        }
    }
}
