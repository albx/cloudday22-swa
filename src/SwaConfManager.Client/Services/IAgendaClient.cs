using SwaConfManager.Shared;

namespace SwaConfManager.Client.Services;

public interface IAgendaClient
{
    Task<IEnumerable<TalkListItemModel>> GetAgendaAsync();

    Task CreateTalkAsync(CreateTalkModel model);

    Task<TalkDetailModel?> GetTalkDetailAsync(Guid talkId);

    Task DeleteTalkAsync(Guid talkId);

    Task RateTalkAsync(Guid talkId, TalkRateModel model);
}
