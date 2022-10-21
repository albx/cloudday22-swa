namespace SwaConfManager.Shared;

public record TalkListItemModel(Guid Id, string Title, string Speaker, TimeSpan StartingTime, TimeSpan EndingTime)
{
    public double Duration => (EndingTime - StartingTime).TotalHours;
}
