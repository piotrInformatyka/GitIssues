namespace GitIssues.Application.Application
{
    public interface IFileStoreService
    {
        Task<bool> WriteToFile(string content, string filename = "test.txt");
        Task<string> ReadFromFile(string filename = "test.txt");
    }
}
