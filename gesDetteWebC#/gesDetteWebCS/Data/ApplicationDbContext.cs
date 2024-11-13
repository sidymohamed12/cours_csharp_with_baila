
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
        public DbSet<Article> Articles { get; set; }
        public DbSet<Dette> Dettes { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Payement> Payements { get; set; }


    }
}