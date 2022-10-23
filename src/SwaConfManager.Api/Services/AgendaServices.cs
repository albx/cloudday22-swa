using SwaConfManager.Core;
using SwaConfManager.Shared;

namespace SwaConfManager.Api.Services;

public class AgendaServices
{
    public AgendaDataManager Data { get; }

    public AgendaServices(AgendaDataManager data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public IEnumerable<TalkListItemModel> GetAllTalks()
        => Data.GetTalks().Select(t => new TalkListItemModel(t.Id, t.Title, t.Speaker, t.StartingTime, t.EndingTime));

    public TalkDetailModel? GetTalkDetails(Guid talkId)
    {
        var talk = Data.GetTalkById(talkId);
        if (talk is null)
        {
            return null;
        }

        var model = new TalkDetailModel
        {
            Id = talk.Id,
            Abstract = talk.Abstract,
            EndingTime = talk.EndingTime,
            Speaker = talk.Speaker,
            StartingTime = talk.StartingTime,
            Title = talk.Title
        };

        var rates = Data.GeTalkRates(talkId);
        if (rates.Any())
        {
            model.Rates = rates.Select(r => new TalkRateModel { Id = r.Id, Author = r.Author, Comment = r.Comment, Rate = r.Rate });
        }

        return model;
    }

    public void DeleteTalk(Guid talkId)
    {
        Data.DeleteTalk(talkId);
    }

    public void CreateTalk(CreateTalkModel model, string userId)
    {
        Data.AddTalk(model.Title, model.Speaker, model.Abstract, model.StartingTime, model.EndingTime, model.IsBreakSlot, userId);
    }

    public void RateTalk(Guid talkId, TalkRateModel model)
    {
        Data.RateTalk(talkId, model.Rate, model.Comment, model.Author);
    }
}
