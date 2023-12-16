using Microsoft.EntityFrameworkCore;

namespace CSProject.Data
{
    public class CSProjectContext : DbContext
    {
        public CSProjectContext (DbContextOptions<CSProjectContext> options) : base(options)
        {

        }

        public DbSet<CSProject.Product> Product { get; set; } = default!;
    }
}
