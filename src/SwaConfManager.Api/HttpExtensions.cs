using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;

namespace SwaConfManager.Api;

public static class HttpExtensions
{
    public static async Task<TModel?> ParseBodyAsync<TModel>(this HttpRequestData request)
    {
        using var reader = new StreamReader(request.Body);
        var requestBody = await reader.ReadToEndAsync();

        var model = JsonSerializer.Deserialize<TModel>(requestBody, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return model;
    }
}
