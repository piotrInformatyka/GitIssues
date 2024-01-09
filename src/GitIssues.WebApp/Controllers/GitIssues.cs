using GitIssues.Application.Infrastructure.Clients.Github;
using Microsoft.AspNetCore.Mvc;

namespace GitIssues.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitIssues : ControllerBase
    {
        private readonly GithubClient _githubClient;

        public GitIssues(GithubClient githubClient)
        {
            _githubClient = githubClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetIssues(string token)
        {
            var results = await _githubClient.GetIssuesAsync("piotrInformatyka", "GitIssues", token);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(string token)
        {
            var result = await _githubClient.CreateIssueAsync("piotrInformatyka", "GitIssues", token);
            return Ok(result);
        }
    }
}
