using Microsoft.AspNetCore.Mvc;
using BookingApp.Services;
using BookingApp.Models;
using Microsoft.Extensions.Logging;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly ILogger<ParticipantsController> _logger;

        public ParticipantsController(IParticipantService participantService, ILogger<ParticipantsController> logger)
        {
            _participantService = participantService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipants()
        {
            try
            {
                List<User> participants = await _participantService.GetAllParticipantsAsync();
                if (participants == null || participants.Count == 0)
                {
                    return NotFound("No participants found.");
                }

                _logger.LogInformation("Retrieved {ParticipantCount} participants.", participants.Count);

                return Ok(participants); // Trả về danh sách participants
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching participants.");
                return StatusCode(500, "Internal server error while fetching participants.");
            }
        }
    }
}
