using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HabitTracker.Models;

namespace HabitTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HabitTracker.Models.Habit> Habit { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between AppUser and Habit
            modelBuilder.Entity<Habit>()
                .HasOne(h => h.AppUser)
                .WithMany(u => u.Habits)
                .HasForeignKey(h => h.AppUserId);
        }
    }
}
