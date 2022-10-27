using Microsoft.Azure.Functions.Worker.Http;
using SwaConfManager.Api.Models;
using System.Text;
using System.Text.Json;

namespace SwaConfManager.Api.Extensions;

public static class HttpRequestDataExtensions
{
    public static ClientPrincipal? GetClientPrincipal(this HttpRequestData request)
    {
        if (!request.Headers.TryGetValues("x-ms-client-principal", out var header))
        {
            return null;
        }

        var data = header!.First();
        var decoded = Convert.FromBase64String(data);
        var json = Encoding.ASCII.GetString(decoded);

        var principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        return principal;
    }
}
