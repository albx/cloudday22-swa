using SwaConfManager.Core.Models;

namespace SwaConfManager.Core;

public class AgendaDataManager
{
    public IEnumerable<Talk> GetTalks() => AgendaStore.Talks;

    public Talk? GetTalkById(Guid talkId) => AgendaStore.Talks.SingleOrDefault(t => t.Id == talkId);

    public IEnumerable<TalkRate> GeTalkRates(Guid talkId) => AgendaStore.TalkRates.Where(t => t.TalkId == talkId);

    public void AddTalk(string title, string speaker, string @abstract, TimeSpan startingTime, TimeSpan endingTime, bool isBreakSlot, string userId)
    {
        var talk = new Talk
        {
            Id = Guid.NewGuid(),
            Title = title,
            Abstract = @abstract,
            EndingTime = endingTime,
            Speaker = speaker,
            StartingTime = startingTime,
            IsBreakSlot = isBreakSlot,
            UserId = userId
        };

        AgendaStore.Talks.Add(talk);
    }

    public void RateTalk(Guid talkId, int rate, string? comment, string? author)
    {
        var talk = AgendaStore.Talks.SingleOrDefault(t => t.Id == talkId);
        if (talk is null)
        {
            throw new ArgumentOutOfRangeException(nameof(talkId));
        }

        if (talk.IsBreakSlot)
        {
            throw new InvalidOperationException("Cannot rate a break slot");
        }

        var talkRate = new TalkRate
        {
            Id = Guid.NewGuid(),
            Author = author,
            Comment = comment,
            Rate = rate,
            TalkId = talkId
        };

        AgendaStore.TalkRates.Add(talkRate);
    }

    public void DeleteTalk(Guid talkId)
    {
        var talk = AgendaStore.Talks.SingleOrDefault(t => t.Id == talkId);
        if (talk is null)
        {
            throw new ArgumentOutOfRangeException(nameof(talkId));
        }

        AgendaStore.Talks.Remove(talk);
        AgendaStore.TalkRates.RemoveAll(r => r.TalkId == talkId);
    }

    internal static class AgendaStore
    {
        public static List<Talk> Talks { get; set; } = new();

        public static List<TalkRate> TalkRates { get; set; } = new();
    }
}
