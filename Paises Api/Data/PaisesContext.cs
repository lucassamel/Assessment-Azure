using Microsoft.EntityFrameworkCore;
using Paises_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paises_Api.Data
{
    public class PaisesContext : DbContext
    {
        public PaisesContext(DbContextOptions<PaisesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Alterar a senha para acessar o banco
            optionsBuilder.UseSqlServer(
                "Server=tcp:lucassamel-db-2020.database.windows.net,1433;Initial Catalog=assessment;" +
                "Persist Security Info=False;User ID=lucassamel;Password=C@dead0Sam3l;" +
                "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
