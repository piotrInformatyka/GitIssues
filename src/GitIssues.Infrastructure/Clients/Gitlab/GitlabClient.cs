using GitIssues.Application.Clients;
using GitIssues.Application.Infrastructure.Clients;
using GitIssues.Application.Infrastructure.Clients.Gitlab;
using GitIssues.Application.Models;
using GitIssues.Infrastructure.Exceptions;
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
        var projectPath = GetProjectPath();
        var response = await _httpClient.GetAsync($"api/v4/projects/{projectPath}/issues");
        var result = await response.DeserializeResponse<IEnumerable<GitlabClientGetIssuesItem>>();

        return result is null ? throw new Exception("Response is empty") : result.Select(x => x.ToIssue());
    }

    public async Task<bool> CreateNewIssueAsync(CreateNewGitIssue issue)
    {
        var (projectPath, content) = GetProjectPathAndRequest(issue.ToGitlabRequest());
        var response = await _httpClient.PostAsync($"/api/v4/projects/{projectPath}/issues", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateIssueAsync(CreateGitIssueItem issue)
    {
        var (projectPath, content) = GetProjectPathAndRequest(issue.ToGitlabRequest());
        var response = await _httpClient.PostAsync($"/api/v4/projects/{projectPath}/issues", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ModifyIssueAsync(ModifyGitIssueItem issue)
    {
        var (projectPath, content) = GetProjectPathAndRequest(issue.ToGitlabRequest());
        var response = await _httpClient.PutAsync($"/api/v4/projects/{projectPath}/issues/{issue.Id}", content);

        return response.IsSuccessStatusCode;
    }

    private string GetProjectPath() => Uri.EscapeDataString($"{_owner}/{_repo}");

    private (string projectPath, StringContent content) GetProjectPathAndRequest<T>(T issue)
    {
        try
        {
            var request = JsonSerializer.Serialize(new { issue });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            return (GetProjectPath(), content);
        }
        catch
        {
            throw new SerializeRequestException(typeof(T).Name);
        }
    }

}
