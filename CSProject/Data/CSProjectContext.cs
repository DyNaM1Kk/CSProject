using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSProject;

namespace CSProject.Data
{
    public class CSProjectContext : DbContext
    {
        public CSProjectContext (DbContextOptions<CSProjectContext> options)
            : base(options)
        {
        }

        public DbSet<CSProject.Product> Products { get; set; } = default!;
    }
}
