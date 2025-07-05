using Microsoft.EntityFrameworkCore;

namespace LunchPoll.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Poll>(x => x.HasKey(e => new { e.EmployeeId, e.RestaurantId }));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Polls)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Polls)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestaurantId);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = "e1", Name = "Alice" },
                new Employee { Id = "e2", Name = "Bob" },
                new Employee { Id = "e3", Name = "Charlie" }
  );

            // Seed Restaurants
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = "r1", Name = "Pizza Place" },
                new Restaurant { Id = "r2", Name = "Sushi Spot" },
                new Restaurant { Id = "r3", Name = "Burger Barn" }
            );
        }




    }
}
