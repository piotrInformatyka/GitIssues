﻿using GitIssues.Application.Application.Clients;
using System.Text.Json;

namespace GitIssues.Application.Application.Commands;

public class ExportIssuesCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;
    private readonly IFileStoreService _fileStoreService;

    public ExportIssuesCommandHandler(IEnumerable<IGitIssueClientStrategy> gitIssueClientStrategies, IFileStoreService fileStoreService)
    {
        _gitIssueClientStrategies = gitIssueClientStrategies;
        _fileStoreService = fileStoreService;
    }

    public async Task<bool> Handle()
    {
        var tasks = _gitIssueClientStrategies.Select(x => x.GetIssuesAsync());
        var results = await Task.WhenAll(tasks);

        var gitIssuesJson = JsonSerializer.Serialize(results.SelectMany(x => x));
        var result = await _fileStoreService.WriteToFile(gitIssuesJson);

        return result;
    }
}
