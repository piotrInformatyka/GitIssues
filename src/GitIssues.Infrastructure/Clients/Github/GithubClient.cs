using GitIssues.Application.Clients;
using GitIssues.Application.Models;
using GitIssues.Infrastructure.Clients.Github;
using GitIssues.Infrastructure.Exceptions;
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

    public async Task<bool> CreateNewIssueAsync(CreateNewIssue request)
    {
        var content = GetContent(request.ToGithubRequest());
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateIssueAsync(CreateIssueItem request)
    {
        var content = GetContent(request.ToGithubRequest());
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ModifyIssueAsync(ModifyIssueItem request)
    {
        var content = GetContent(request.ToGithubRequest());
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues/{request.Id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CloseIssueAsync(CloseIssue request)
    {
        var content = GetContent(request.ToGithubRequest());
        var response = await _httpClient.PostAsync($"/repos/{_owner}/{_repo}/issues/{request.Id}", content);
        return response.IsSuccessStatusCode;
    }

    private static StringContent GetContent<T>(T issue)
    {
        try
        {
            var request = JsonSerializer.Serialize(issue);
            return new StringContent(request, Encoding.UTF8, "application/json");
        }
        catch
        {
            throw new SerializeRequestException(typeof(T).Name);
        }
    }
}
