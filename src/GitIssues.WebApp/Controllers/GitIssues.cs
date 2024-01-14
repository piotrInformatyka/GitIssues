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
        private readonly CloseIssueCommandHandler _closeIssueCommandHandler;

        public GitIssues(AddNewIssueCommandHandler addNewIssueCommandHandler, 
            ExportIssuesCommandHandler exportIssuesCommandHandler,
            ModifyIssueCommandHandler modifyIssueCommandHandler,
            ImportIssuesCommandHandler importIssuesCommandHandler,
            CloseIssueCommandHandler closeIssueCommandHandler)
        {
            _addNewIssueCommandHandler = addNewIssueCommandHandler;
            _exportIssuesCommandHandler = exportIssuesCommandHandler;
            _modifyIssueCommandHandler = modifyIssueCommandHandler;
            _importIssuesCommandHandler = importIssuesCommandHandler;
            _closeIssueCommandHandler = closeIssueCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(string title, string description, string repositoryType)
        {
            var result = await _addNewIssueCommandHandler.Handle(new AddNewIssueCommand(title, description, repositoryType));
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



        [HttpPatch("issueId")]
        public async Task<IActionResult> ModifyIssue(int issueId, ModifyIssueCommand request)
        {
            var result = await _modifyIssueCommandHandler.Handle(request with { IssueId = issueId });
            return Ok(result);
        }

        [HttpPatch("issueId/close")]
        public async Task<IActionResult> CloseIssue(int issueId, CloseIssueCommand request)
        {
            var result = await _closeIssueCommandHandler.Handle(request with { IssueId = issueId });
            return Ok(result);
        }
    }
}
