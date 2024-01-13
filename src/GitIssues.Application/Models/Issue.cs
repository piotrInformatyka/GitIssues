namespace GitIssues.Application.Models;

public class Issue
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public IssueState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    public RepositoryType RepositoryType { get; set; }
}
