using GitIssues.Application.Services;
using Microsoft.Extensions.Hosting;

namespace GitIssues.Infrastructure.Services;

internal sealed class FileStorageService : IFileStoreService
{
    private readonly string _filePath;

    public FileStorageService(IHostEnvironment webHostEnvironment)
    {
        _filePath = webHostEnvironment.ContentRootPath;
    }

    public async Task<string> ReadFromFile(string filename)
    {
        try
        {
            string content = await File.ReadAllTextAsync(Path.Combine(_filePath, filename));
            return content;
        }
        catch
        {
            return string.Empty;
        }
    }

    public async Task<bool> WriteToFile(string content, string filename)
    {
        try
        {
            await File.WriteAllTextAsync(Path.Combine(_filePath, filename), content);
            return true;
        }
        catch
        {
            //log or throw
        }
        return false;
    }
}
