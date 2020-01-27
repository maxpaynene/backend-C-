using ApiSwaggers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSwaggers.ApiContext
{
    public class Api:DbContext
    {
        public Api(DbContextOptions<Api> options):base(options)
        {

        }

        public DbSet<Cliente> cliente { set; get; }
        public DbSet<Correo> correo { get; set; }
        public DbSet<Chat> chat { get; set; }
        public DbSet<Clave> clave { get; set; }
        public DbSet<Ruta> ruta { get; set; }
        public DbSet<Entrega> entrega { get; set; }

    }
}
