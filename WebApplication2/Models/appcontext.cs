using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class appcontext :IdentityDbContext<applicationuser>
    {
        public DbSet<product> products { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<category> categories  { get; set; }
        public DbSet<supplier> suppliers { get; set; }
        public appcontext(DbContextOptions<appcontext> options):base(options) { }       
        public appcontext() : base() { }
    }
}
