using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContext : DbContext
    {

        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options) //Constructor. Al recibir el options es posible que este objeto pueda ser inicializado desde el webapi
        {

        }

        //Clases que se convertiran en entidades
        public DbSet<Producto> Producto { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Marca> Marca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Para que la configuración se aplique
        }


    }
}
