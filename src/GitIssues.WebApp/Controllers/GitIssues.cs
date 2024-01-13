using GitIssues.Application.Commands;
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
        private readonly ImportIssuesCommandHandler _importIssuesCommandHandler;

        public GitIssues(AddNewIssueCommandHandler addNewIssueCommandHandler, 
            ExportIssuesCommandHandler exportIssuesCommandHandler,
            ModifyIssueCommandHandler modifyIssueCommandHandler,
            ImportIssuesCommandHandler importIssuesCommandHandler)
        {
            _addNewIssueCommandHandler = addNewIssueCommandHandler;
            _exportIssuesCommandHandler = exportIssuesCommandHandler;
            _modifyIssueCommandHandler = modifyIssueCommandHandler;
            _importIssuesCommandHandler = importIssuesCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue()
        {
            var result = await _addNewIssueCommandHandler.Handle(new AddNewIssueCommand("Gitlab - 3 - created by api", "Test", "GitLab"));
            return Ok(result);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportIssues()
        {
            var result = await _exportIssuesCommandHandler.Handle();
            return Ok(result);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportIssues(ImportIssuesCommand request)
        {
            var result = await _importIssuesCommandHandler.Handle(request);
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
