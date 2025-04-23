using BookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", AvatarUrl = "https://example.com/avatars/johndoe.png" },
                new User { UserId = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "0987654321", AvatarUrl = "https://example.com/avatars/janesmith.png" },
                new User { UserId = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com", Phone = "1122334455", AvatarUrl = "https://example.com/avatars/alicejohnson.png" }
            );

            // Seed data for Events
            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, Duration = "30 MIN", Time = "10:00 AM", Date = "2025-03-13" },
                new Event { EventId = 2, Duration = "45 MIN", Time = "02:00 PM", Date = "2025-03-13" },
                new Event { EventId = 3, Duration = "1 HOUR", Time = "06:00 PM", Date = "2025-03-14" }
            );

            // Seed data for EventUsers
            modelBuilder.Entity<EventUser>().HasData(
                new EventUser { EventUserId = 1, EventId = 1, UserId = 1, Role = "Organizer" },
                new EventUser { EventUserId = 2, EventId = 1, UserId = 2, Role = "Attendee" },
                new EventUser { EventUserId = 3, EventId = 2, UserId = 3, Role = "Attendee" },
                new EventUser { EventUserId = 4, EventId = 3, UserId = 1, Role = "Attendee" },
                new EventUser { EventUserId = 5, EventId = 3, UserId = 2, Role = "Organizer" }
            );
        }
    }
}
