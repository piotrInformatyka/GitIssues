using GitIssues.Application.Infrastructure.Exceptions;
using System.Text.Json;

namespace GitIssues.Application.Infrastructure.Clients;

public static class HttpMessageResponseExtensions
{
    public static async Task<T?> DeserializeResponse<T>(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<T>(content);
            }
            catch
            {
                throw new DeserializeResponseException(typeof(T).Name, content);
            }
        }
        return default;
    }
}
