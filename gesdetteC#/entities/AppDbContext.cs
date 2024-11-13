using Microsoft.EntityFrameworkCore;

namespace gesdette.entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Dette> Dettes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Payement> Payements { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=gestion_dette_cours_CS;Username=postgres;Password=SMS;Port=5432");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // base.OnModelCreating(modelBuilder);

            // Configurer la colonne pour Role comme un entier
            // modelBuilder.Entity<User>()
            //     .Property(u => u.Role)
            //     .HasConversion<int>();

            // Configure Client and User relationship
            // modelBuilder.Entity<Client>()
            //     .HasOne(c => c.User)
            //     .WithOne(u => u.Client)
            //     .HasForeignKey<Client>(c => c.User.Id);

            // // Configure Dette and Client relationship
            // modelBuilder.Entity<Dette>()
            //     .HasOne(d => d.ClientD)
            //     .WithMany(c => c.Dettes)
            //     .HasForeignKey(d => d.ClientD.Id);

            // // Configure Detail and Dette, Article relationships
            // modelBuilder.Entity<Detail>()
            //     .HasOne(d => d.Dette)
            //     .WithMany(dt => dt.Details)
            //     .HasForeignKey(d => d.Dette.Id);

            // modelBuilder.Entity<Detail>()
            //     .HasOne(d => d.Article)
            //     .WithMany(a => a.Details)
            //     .HasForeignKey(d => d.Article.Id);

        }
    }
}