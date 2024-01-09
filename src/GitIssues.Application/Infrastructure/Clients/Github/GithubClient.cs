using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GitIssues.Application.Infrastructure.Clients.Github;

public class GithubClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _token;

    public GithubClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["githubApiUrl"] ?? string.Empty;
        _token = configuration["githubToken"] ?? string.Empty;
    }

    public async Task<IEnumerable<string>> GetIssuesAsync(string owner, string repo, string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubIssueCreator");

        var response = await _httpClient.GetAsync($"{_baseUrl}/repos/{owner}/{repo}/issues");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var issues = JsonSerializer.Deserialize<IEnumerable<GithubIssueItem>>(content);

        return issues.Select(x => x.Title);
    }

    public async Task<bool> CreateIssueAsync(string owner, string repo, string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubIssueCreator");
        var issueData = new
        {
            owner,
            repo,
            title = "3th issue - created by api",
            body = "3th issue - created by api - description"
        };

        var issueJson = JsonSerializer.Serialize(issueData);
        var content = new StringContent(issueJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/repos/{owner}/{repo}/issues", content);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }
}
