using GitIssues.Application.Application;
using GitIssues.Application.Application.Clients;
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
        services.AddScoped<ExportIssuesCommandHandler>();
        services.AddScoped<ModifyIssueCommandHandler>();
        services.AddScoped<IFileStoreService, FileStorageService>();

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
