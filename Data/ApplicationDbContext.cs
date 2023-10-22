using Microsoft.EntityFrameworkCore;
using HelloWorld.Models;

namespace HelloWorld.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Empleo> Empleos { get; set; }
    }
}
