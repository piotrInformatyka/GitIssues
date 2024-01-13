using GitIssues.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GitIssues.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<AddNewIssueCommandHandler>();
        services.AddScoped<ExportIssuesCommandHandler>();
        services.AddScoped<ImportIssuesCommandHandler>();
        services.AddScoped<ModifyIssueCommandHandler>();

        return services;
    }
}
