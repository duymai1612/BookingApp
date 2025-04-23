using BookingApi.MockData;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly ILogger<TimeSlotsController> _logger;

    public TimeSlotsController(ILogger<TimeSlotsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetByDate([FromQuery] DateTime date)
    {
        List<TimeSlot> slots = StaticDataStore.TimeSlots
            .Where(s => s.Date.Date == date.Date)
            .ToList();

        _logger.LogInformation("📅 Found {Count} time slots for date {Date}", slots.Count, date.ToShortDateString());

        return Ok(slots);
    }
}
