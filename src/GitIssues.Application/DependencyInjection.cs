using GitIssues.Application.Application.Commands;
using GitIssues.Application.Infrastructure.Clients;
using GitIssues.Application.Infrastructure.Clients.Github;
using GitIssues.Application.Infrastructure.Clients.Gitlab;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace GitIssues.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AddNewIssueCommandHandler>();

        services.AddHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<GithubClient>((sp, client) =>
        {
            client.BaseAddress = new Uri(configuration["githubApiUrl"] ?? string.Empty);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["githubToken"] ?? string.Empty}");
            client.DefaultRequestHeaders.Add("User-Agent", "GitHubIssueCreator");
        });
        services.AddHttpClient<GitlabClient>((sp, client) =>
        {
            client.BaseAddress = new Uri(configuration["gitlabApiUrl"] ?? string.Empty);
            client.DefaultRequestHeaders.Add("Private-Token", configuration["gitlabToken"] ?? string.Empty);
        });

        services.AddScoped<IGitIssueClientStrategy, GithubClient>();
        services.AddScoped<IGitIssueClientStrategy, GitlabClient>();

        return services;
    }
}
