using GitIssues.Application.Infrastructure.Clients.Github;
using GitIssues.Infrastructure.Clients.Gitlab;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GitIssues.Application.Clients;
using GitIssues.Infrastructure.Services;
using GitIssues.Application.Services;


namespace GitIssues.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IFileStoreService, FileStorageService>();
        services.AddHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IGitIssueClientStrategy, GithubClient>();
        services.AddHttpClient<IGitIssueClientStrategy, GitlabClient>();

        return services;
    }
}
