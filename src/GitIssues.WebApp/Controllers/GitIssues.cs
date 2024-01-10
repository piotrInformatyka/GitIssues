using GitIssues.Application.Application.Commands;
using GitIssues.Application.Infrastructure.Clients.Github;
using GitIssues.Application.Infrastructure.Clients.Gitlab;
using Microsoft.AspNetCore.Mvc;

namespace GitIssues.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitIssues : ControllerBase
    {
        private readonly AddNewIssueCommandHandler _addNewIssueCommandHandler;

        public GitIssues(AddNewIssueCommandHandler addNewIssueCommandHandler)
        {
            _addNewIssueCommandHandler = addNewIssueCommandHandler;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetIssues()
        //{
        //    ////var results = await _githubClient.GetIssuesAsync("piotrInformatyka", "GitIssues");
        //    //var results = await _gitlabClient.GetIssuesAsync("piotrInformatyka", "GitIssues");
        //    //return Ok(results);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateIssue()
        {
            var result = await _addNewIssueCommandHandler.Handle(new AddNewIssueCommand("Test", "Test", "GitHub"));
            return Ok(result);
        }
    }
}
