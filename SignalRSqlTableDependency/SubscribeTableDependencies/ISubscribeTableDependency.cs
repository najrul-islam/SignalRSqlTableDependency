namespace SignalRSqlTableDependency.SubscribeTableDependencies
{
    public interface ISubscribeTableDependency
    {
        Task SubscribeTableDependency(string connectionString);
    }
}
