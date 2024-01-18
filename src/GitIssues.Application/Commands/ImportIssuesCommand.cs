using GitIssues.Application.Clients;
using GitIssues.Application.Models;
using GitIssues.Application.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GitIssues.Application.Commands;

public record ImportIssuesCommand(string RepositoryType);

public class ImportIssuesCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;
    private readonly IFileStoreService _fileStoreService;
    private readonly ILogger<ImportIssuesCommandHandler> _logger;

    public ImportIssuesCommandHandler(IEnumerable<IGitIssueClientStrategy> strategies, 
        IFileStoreService fileStoreService, 
        ILogger<ImportIssuesCommandHandler> logger)
    {
        _fileStoreService = fileStoreService;
        _gitIssueClientStrategies = strategies;
        _logger = logger;
    }

    public async Task<bool> Handle(ImportIssuesCommand command)
    {
        var issuesJson = await _fileStoreService.ReadFromFile();
        var issues = JsonSerializer.Deserialize<IEnumerable<Issue>>(issuesJson);
        if (issues is null || !issues.Any())
            throw new Exception("Imported list of issues is empty");

        var tasks = issues.Select(x => (
            task: _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType)).CreateNewIssueAsync(new(x.Body, x.Title)), 
            issueId: x.Id));
        var results = await Task.WhenAll(tasks.Select(x => x.task));

        if(results.Any(x => !x))
        {
            var failedIssues = tasks.Where(x => !x.task.Result).Select(x => x.issueId);
            _logger.LogError($"Failed to import issues with ids: {string.Join(',', failedIssues)}");
        }
        return results.All(x => x);
    }
}