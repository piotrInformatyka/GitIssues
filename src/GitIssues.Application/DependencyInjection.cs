using GitIssues.Application.Infrastructure.Clients.Github;
using Microsoft.Extensions.DependencyInjection;

namespace GitIssues.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddHttpClient<GithubClient>();

        return services;
    }
}
