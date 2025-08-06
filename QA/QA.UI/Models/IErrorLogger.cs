namespace QA.UI.Models
{
    public interface IErrorLogger
    {
        Task LogError(Exception exception, string location);
        void LogInformation(string message, string location);
    }
}
