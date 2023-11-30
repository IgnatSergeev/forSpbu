namespace Conference.Data;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public bool IsSpeaker { get; set; }
}