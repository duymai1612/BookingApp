public class Participant
{
    public int ParticipantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Time { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string Timezone { get; set; } = string.Empty;
}
