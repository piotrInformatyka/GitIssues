using GitIssues.Application.Application.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace GitIssues.Application.Infrastructure.Clients.Gitlab;

public class GitlabClient : IGitIssueClientStrategy
{
    private readonly HttpClient _httpClient;
    private readonly string _owner;
    private readonly string _repo;

    public GitlabClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _owner = configuration["gitlab:gitlabOwner"] ?? string.Empty;
        _repo = configuration["gitlab:gitlabRepo"] ?? string.Empty;
    }

    public bool CanBeApplied(RepositoryType repositoryType) => repositoryType == RepositoryType.GitLab;

    public async Task<IEnumerable<Issue>> GetIssuesAsync()
    {
        var projectPath = Uri.EscapeDataString($"{_owner}/{_repo}");
        var response = await _httpClient.GetAsync($"/api/v4/projects/{Uri.EscapeDataString(projectPath)}/issues");
        var result = await response.DeserializeResponse<IEnumerable<GitlabClientGetIssuesItem>>();
       
        return result is null ? throw new Exception("Response is empty") : result.Select(x => x.ToIssue());
    }

    public async Task<bool> CreateIssueAsync(Issue issue)
    {
        var projectPath = Uri.EscapeDataString($"{_owner}/{_repo}");
        var request = JsonSerializer.Serialize(issue.ToGitlabRequest());
        var content = new StringContent(request, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/api/v4/projects/{projectPath}/issues", content);

        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }
}
