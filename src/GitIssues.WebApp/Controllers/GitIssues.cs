using GitIssues.Application.Application.Commands;
using GitIssues.Application.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitIssues.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitIssues : ControllerBase
    {
        private readonly AddNewIssueCommandHandler _addNewIssueCommandHandler;
        private readonly ExportIssuesCommandHandler _exportIssuesCommandHandler;
        private readonly ModifyIssueCommandHandler _modifyIssueCommandHandler;

        public GitIssues(AddNewIssueCommandHandler addNewIssueCommandHandler, 
            ExportIssuesCommandHandler exportIssuesCommandHandler,
            ModifyIssueCommandHandler modifyIssueCommandHandler)
        {
            _addNewIssueCommandHandler = addNewIssueCommandHandler;
            _exportIssuesCommandHandler = exportIssuesCommandHandler;
            _modifyIssueCommandHandler = modifyIssueCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue()
        {
            var result = await _addNewIssueCommandHandler.Handle(new AddNewIssueCommand("Test", "Test", "GitHub"));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ExportIssues()
        {
            var result = await _exportIssuesCommandHandler.Handle();
            return Ok(result);
        }

        [HttpPost("issueId")]
        public async Task<IActionResult> ModifyIssue(int issueId, ModifyIssueCommand request)
        {
            var result = await _modifyIssueCommandHandler.Handle(request with { IssueId = issueId });
            return Ok(result);
        }
    }
}
