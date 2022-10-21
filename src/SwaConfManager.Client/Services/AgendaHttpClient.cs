using SwaConfManager.Shared;
using System.Net.Http.Json;

namespace SwaConfManager.Client.Services;

public class AgendaHttpClient : IAgendaClient
{
    public AgendaHttpClient(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public HttpClient Client { get; }

    public async Task CreateTalkAsync(CreateTalkModel model)
    {
        var response = await Client.PostAsJsonAsync("api/talk", model);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTalkAsync(Guid talkId)
    {
        var response = await Client.DeleteAsync($"api/talk/{talkId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<TalkListItemModel>> GetAgendaAsync()
    {
        var items = await Client.GetFromJsonAsync<IEnumerable<TalkListItemModel>>("api/agenda");
        return items ?? Array.Empty<TalkListItemModel>();
    }

    public async Task<TalkDetailModel?> GetTalkDetailAsync(Guid talkId)
    {
        var talk = await Client.GetFromJsonAsync<TalkDetailModel>($"api/talk/{talkId}");
        return talk;
    }

    public async Task RateTalkAsync(Guid talkId, TalkRateModel model)
    {
        var response = await Client.PutAsJsonAsync($"api/talk/{talkId}/rate", model);
        response.EnsureSuccessStatusCode();
    }
}
