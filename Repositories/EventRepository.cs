using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;

namespace BookingApp.Repositories
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event newEvent);
        Task AddEventUserAsync(int eventId, int userId, string role);
        Task<List<Event>> GetEventsByDateAsync(DateTime date);
    }

    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEventAsync(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task AddEventUserAsync(int eventId, int userId, string role)
        {
            // Check if the event exists
            User? user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} does not exist.");
            }

            // Add user to event with the specified role
            EventUser eventUser = new EventUser
            {
                EventId = eventId,
                UserId = userId,
                Role = role
            };

            await _context.EventUsers.AddAsync(eventUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetEventsByDateAsync(DateTime date)
        {
            return await _context.Events
                .Where(e => e.Date == date.ToString("yyyy-MM-dd"))
                .Include(e => e.EventUsers)
                .ThenInclude(eu => eu.User)
                .ToListAsync();
        }
    }
}
