namespace SwaConfManager.Shared;

public class TalkDetailModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Speaker { get; set; } = string.Empty;

    public string Abstract { get; set; } = string.Empty;

    public TimeSpan StartingTime { get; set; }

    public TimeSpan EndingTime { get; set; }

    public IEnumerable<TalkRateModel> Rates { get; set; } = Enumerable.Empty<TalkRateModel>();

    public double Duration => (EndingTime - StartingTime).TotalHours;
}
