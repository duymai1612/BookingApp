namespace BookingApi.MockData;

public static class StaticDataStore
{
    public static List<TimeSlot> TimeSlots { get; set; } = GenerateDummySlots();

    private static List<TimeSlot> GenerateDummySlots()
    {
        List<TimeSlot> slots = new();
        TimeSpan startTime = new(9, 0, 0);
        TimeSpan endTime = new(12, 0, 0);
        TimeSpan interval = new(0, 15, 0);

        for (int dayOffset = 0; dayOffset < 7; dayOffset++)
        {
            DateTime date = DateTime.Today.AddDays(dayOffset);
            for (TimeSpan t = startTime; t < endTime; t += interval)
            {
                DateTime time = date.Add(t);
                slots.Add(new TimeSlot
                {
                    SlotId = slots.Count + 1,
                    Date = date,
                    Time = time.ToString("hh:mm tt")
                });
            }
        }

        return slots;
    }
}