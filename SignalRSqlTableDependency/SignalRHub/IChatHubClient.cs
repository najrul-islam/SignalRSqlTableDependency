namespace SignalRSqlTableDependency.SignalRHub
{
    public interface IChatHubClient
    {
        Task onProductUpdate(string message);
    }
}
