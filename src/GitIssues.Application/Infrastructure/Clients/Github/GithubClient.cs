using GitIssues.Application.Application.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GitIssues.Application.Infrastructure.Clients.Github;

public class GithubClient : IGitIssueClientStrategy
{
    private readonly HttpClient _httpClient;
    private readonly string _owner;
    private readonly string _repo;

    public GithubClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _owner = configuration["github:githubOwner"] ?? string.Empty;
        _repo = configuration["github:githubRepo"] ?? string.Empty;
    }

    public bool CanBeApplied(RepositoryType repositoryType) => repositoryType == RepositoryType.GitHub;

    public async Task<IEnumerable<Issue>> GetIssuesAsync()
    {
        var response = await _httpClient.GetAsync($"/repos/{_owner}/{_repo}/issues");
        var result = await response.DeserializeResponse<IEnumerable<GithubIssueItem>>();

        return result is null? throw new Exception("Response is empty") : result.Select(x => x.ToIssue());
    }

    public async Task<bool> CreateIssueAsync(Issue issue)
    {
        var request = JsonSerializer.Serialize(issue.ToGithubRequest());
        var content = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues", content);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }
}
