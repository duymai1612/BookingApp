using BookingApi.MockData;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly ILogger<ParticipantsController> _logger;

    public ParticipantsController(ILogger<ParticipantsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Add([FromBody] Participant participant)
    {
        participant.ParticipantId = StaticDataStore.Participants.Count + 1;
        StaticDataStore.Participants.Add(participant);

        _logger.LogInformation("✅ Added participant: {Name} at {Date} {Time}", participant.Name, participant.Date.ToShortDateString(), participant.Time);

        return Ok(new { message = "Participant added", participantId = participant.ParticipantId });
    }

    [HttpGet]
    public IActionResult GetByDate([FromQuery] DateTime date)
    {
        var list = StaticDataStore.Participants
            .Where(p => p.Date.Date == date.Date)
            .ToList();

        _logger.LogInformation("🔎 Retrieved {Count} participants for {Date}", list.Count, date.ToShortDateString());

        return Ok(list);
    }
}
