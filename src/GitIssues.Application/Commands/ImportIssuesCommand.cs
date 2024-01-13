using GitIssues.Application.Clients;
using GitIssues.Application.Models;
using GitIssues.Application.Services;
using System.Text.Json;

namespace GitIssues.Application.Commands;

public record ImportIssuesCommand(string RepositoryType);

public class ImportIssuesCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;
    private readonly IFileStoreService _fileStoreService;

    public ImportIssuesCommandHandler(IEnumerable<IGitIssueClientStrategy> strategies, IFileStoreService fileStoreService)
    {
        _fileStoreService = fileStoreService;
        _gitIssueClientStrategies = strategies;
    }

    public async Task<bool> Handle(ImportIssuesCommand command)
    {
        var issuesJson = await _fileStoreService.ReadFromFile();
        var issues = JsonSerializer.Deserialize<IEnumerable<Issue>>(issuesJson);
        if (issues is null)
            throw new Exception("Issues are null");

        var tasks = issues.Select(x => _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType)).CreateNewIssueAsync(new(x.Body, x.Title)));
        var results = await Task.WhenAll(tasks);
        //log dead issues

        return results.All(x => x);
    }
}