namespace SwaConfManager.Core.Models;

public class Talk
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Speaker { get; set; } = string.Empty;

    public string Abstract { get; set; } = string.Empty;

    public TimeSpan StartingTime { get; set; }

    public TimeSpan EndingTime { get; set; }

    public bool IsBreakSlot { get; set; }

    public string UserId { get; set; } = string.Empty;
}
