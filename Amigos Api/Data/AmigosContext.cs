using Amigos_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amigos_Api.Data
{
    public class AmigosContext : DbContext
    {
        public AmigosContext(DbContextOptions<AmigosContext> options) : base(options)
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

        public DbSet<Amigo> Amigos { get; set; }
    }
}
