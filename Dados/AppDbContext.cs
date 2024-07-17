using APIDesafio.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDesafio.Dados
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");


        public DbSet<Usuario> Usuarios { get; set; }
    }
}
