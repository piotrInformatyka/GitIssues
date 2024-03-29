﻿using GitIssues.Application.Clients;

namespace GitIssues.Application.Commands;

public record AddNewIssueCommand(string Title, string Body, string RepositoryType);

public class AddNewIssueCommandHandler
{
    private readonly IEnumerable<IGitIssueClientStrategy> _gitIssueClientStrategies;

    public AddNewIssueCommandHandler(IEnumerable<IGitIssueClientStrategy> gitIssueClientStrategies)
    {
        _gitIssueClientStrategies = gitIssueClientStrategies;
    }

    public async Task<bool> Handle(AddNewIssueCommand command)
    {
        var strategy = _gitIssueClientStrategies.Single(x => x.CanBeApplied(command.RepositoryType));

        var request = new CreateNewIssue(command.Body, command.Title);

        return await strategy.CreateNewIssueAsync(request);
    }
}
