using GitIssues.Application.Application.Models;
using GitIssues.Application.Infrastructure.Clients;

namespace GitIssues.Application.Application.Commands;

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
        var strategy = _gitIssueClientStrategies.SingleOrDefault(x => x.CanBeApplied(command.RepositoryType));

        var issue = new Issue
        {
            Title = command.Title,
            Body = command.Body,
        };

        var result = await strategy.CreateIssueAsync(issue);
        return result;
    }
}
