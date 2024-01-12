using GitIssues.Application.Clients;
using GitIssues.Application.Infrastructure.Clients;
using GitIssues.Application.Infrastructure.Clients.Gitlab;
using GitIssues.Application.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GitIssues.Infrastructure.Clients.Gitlab;

internal sealed class GitlabClient : IGitIssueClientStrategy
{
    private readonly HttpClient _httpClient;
    private readonly string _owner;
    private readonly string _repo;

    public GitlabClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["gitlab:gitlabApiUrl"] ?? string.Empty);
        _httpClient.DefaultRequestHeaders.Add("Private-Token", configuration["gitlab:gitlabToken"] ?? string.Empty);
        _owner = configuration["gitlab:gitlabOwner"] ?? string.Empty;
        _repo = configuration["gitlab:gitlabRepo"] ?? string.Empty;
    }

    public bool CanBeApplied(RepositoryType repositoryType) => repositoryType == RepositoryType.GitLab;

    public async Task<IEnumerable<Issue>> GetIssuesAsync()
    {
        var projectPath = Uri.EscapeDataString($"{_owner.ToLower()}/{_repo.ToLower()}");
        var response = await _httpClient.GetAsync($"api/v4/projects/{projectPath}/issues");
        var result = await response.DeserializeResponse<IEnumerable<GitlabClientGetIssuesItem>>();

        return result is null ? throw new Exception("Response is empty") : result.Select(x => x.ToIssue());
    }

    public async Task<bool> CreateNewIssueAsync(CreateNewGitIssue issue)
    {
        var projectPath = Uri.EscapeDataString($"{_owner}/{_repo}");
        var request = JsonSerializer.Serialize(issue.ToGitlabRequest());
        var content = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/api/v4/projects/{projectPath}/issues", content);

        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<bool> CreateIssueAsync(CreateGitIssueItem issue)
    {
        var projectPath = Uri.EscapeDataString($"{_owner}/{_repo}");
        var request = JsonSerializer.Serialize(issue.ToGitlabRequest());
        var content = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/api/v4/projects/{projectPath}/issues", content);

        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }

    public Task<bool> ModifyIssueAsync(ModifyGitIssueItem request)
    {
        throw new NotImplementedException();
    }
}
