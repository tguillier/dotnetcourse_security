using Microsoft.EntityFrameworkCore;

namespace ApiApplication1.Models
{
    public class SampleContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User { Id = 1, Username = "User1", Password = "P@ssw0rd1" },
                    new User { Id = 2, Username = "User2", Password = "P@ssw0rd2" },
                    new User { Id = 3, Username = "User3", Password = "P@ssw0rd3" },
                    new User { Id = 4, Username = "User4", Password = "P@ssw0rd4" },
                    new User { Id = 5, Username = "User5", Password = "P@ssw0rd5" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
