
using Microsoft.EntityFrameworkCore;
using gesDetteWebCS.Models;

namespace gesDetteWebCS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Article> Article { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=gestion_dette_cours_CS;Username=postgres;Password=SMS;Port=5432");
        }


    }
}