using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwaConfManager.Api.Services;
using SwaConfManager.Core;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((ctx, services) =>
    {
        services
            .AddScoped<AgendaDataManager>()
            .AddScoped<AgendaServices>();
    })
    .Build();

SeedInitialData(host);

host.Run();

#region Seed
void SeedInitialData(IHost host)
{
    var agenda = host.Services.GetRequiredService<AgendaServices>();
    var start = 8;

    var userId = Guid.NewGuid().ToString();

    for (int i = 1; i <= 5; i++)
    {
        ++start;
        agenda.CreateTalk(new SwaConfManager.Shared.CreateTalkModel
        {
            Title = $"talk #{i}",
            Abstract = $"Abstract of talk #{i}",
            StartingTime = new TimeSpan(start, 0, 0),
            EndingTime = new TimeSpan(start + 1, 0, 0),
            IsBreakSlot = i == 3,
            Speaker = $"Speaker #{i}"
        }, userId);
    }
}
#endregion