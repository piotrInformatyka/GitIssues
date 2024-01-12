using GitIssues.Application.Clients;
using GitIssues.Application.Models;
using GitIssues.Infrastructure.Clients.Github;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GitIssues.Application.Infrastructure.Clients.Github;

internal sealed class GithubClient : IGitIssueClientStrategy
{
    private readonly HttpClient _httpClient;
    private readonly string _owner;
    private readonly string _repo;

    public GithubClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["github:githubApiUrl"] ?? string.Empty);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["github:githubToken"] ?? string.Empty}");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubIssueCreator");
        _owner = configuration["github:githubOwner"] ?? string.Empty;
        _repo = configuration["github:githubRepo"] ?? string.Empty;
    }

    public bool CanBeApplied(RepositoryType repositoryType) => repositoryType == RepositoryType.GitHub;

    public async Task<IEnumerable<Issue>> GetIssuesAsync()
    {
        var response = await _httpClient.GetAsync($"/repos/{_owner}/{_repo}/issues");
        var result = await response.DeserializeResponse<IEnumerable<GithubIssueItemResponse>>();

        return result is null ? throw new Exception("Response is empty") : result.Select(x => x.ToIssue());
    }

    public async Task<bool> CreateNewIssueAsync(CreateNewGitIssue issue)
    {
        var request = JsonSerializer.Serialize(issue.ToGithubRequest());

        var content = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues", content);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<bool> CreateIssueAsync(CreateGitIssueItem request)
    {
        var requst = JsonSerializer.Serialize(request.ToGithubRequest());

        var content = new StringContent(requst, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues", content);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<bool> ModifyIssueAsync(ModifyGitIssueItem request)
    {
        var requst = JsonSerializer.Serialize(request.ToGithubRequest());
        var content = new StringContent(requst, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues/{request.Id}", content);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }
}
