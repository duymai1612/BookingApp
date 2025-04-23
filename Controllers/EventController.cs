using Microsoft.AspNetCore.Mvc;
using BookingApp.Models;
using BookingApp.Services;
using Microsoft.Extensions.Logging;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventManagementService _eventManagementService;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventManagementService eventManagementService, ILogger<EventController> logger)
        {
            _eventManagementService = eventManagementService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest request)
        {
            if (request == null || request.Participants == null || request.Participants.Count == 0)
            {
                _logger.LogWarning("Attempted to create an event with no participants.");
                return BadRequest("Event and Participants are required.");
            }

            Event newEvent = new Event
            {
                Duration = request.Duration,
                Time = request.Time,
                Date = request.Date
            };

            try
            {
                await _eventManagementService.AddEventAsync(newEvent, request.Participants);

                _logger.LogInformation("Event created successfully with {ParticipantCount} participants.", request.Participants.Count);

                return Ok(new { message = "Event and Participants added successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating event.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetEventsByDate([FromQuery] DateTime date)
        {
            try
            {
                _logger.LogInformation("Fetching events for date: {Date}", date.ToShortDateString());

                List<Event> events = await _eventManagementService.GetEventsByDateAsync(date);

                if (events == null || events.Count == 0)
                {
                    _logger.LogWarning("No events found for date: {Date}", date.ToShortDateString());
                    return NotFound("No events found for this date.");
                }

                _logger.LogInformation("Retrieved {EventCount} events for date: {Date}", events.Count, date.ToShortDateString());

                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching events by date.");
                return StatusCode(500, "Internal server error while fetching events.");
            }
        }
    }

    public class EventRequest
    {
        public string Duration { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public List<int> Participants { get; set; }  // Danh sách chỉ có userId
    }
}
