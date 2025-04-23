using BookingApp.Models;
using BookingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Services
{
    public interface IEventManagementService
    {
        Task AddEventAsync(Event newEvent, List<int> participantIds);
        Task<List<Event>> GetEventsByDateAsync(DateTime date);
    }

    public class EventManagementService : IEventManagementService
    {
        private readonly IEventRepository _eventRepository;

        public EventManagementService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddEventAsync(Event newEvent, List<int> participantIds)
        {
            // Add the new event to the database
            await _eventRepository.AddEventAsync(newEvent);

            // Add participants to the event
            foreach (int userId in participantIds)
            {
                await _eventRepository.AddEventUserAsync(newEvent.EventId, userId, "Attendee");
            }
        }

        public async Task<List<Event>> GetEventsByDateAsync(DateTime date)
        {
            return await _eventRepository.GetEventsByDateAsync(date);
        }
    }
}
