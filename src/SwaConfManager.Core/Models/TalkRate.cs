namespace SwaConfManager.Core.Models;

public class TalkRate
{
    public Guid Id { get; set; }

    public Guid TalkId { get; set; }

    public int Rate { get; set; }

    public string? Comment { get; set; }

    public string? Author { get; set; }
}
