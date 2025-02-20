using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_NUMEROS_CARNETS.Models
{
    public class BlogDBContext : DbContext
    {
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
        }
    }
}
