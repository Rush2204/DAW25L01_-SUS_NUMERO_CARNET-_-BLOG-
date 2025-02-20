using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_NUMEROS_CARNETS.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        }

        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
    }
}
