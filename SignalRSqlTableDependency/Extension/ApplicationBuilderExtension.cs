using SignalRSqlTableDependency.SubscribeTableDependencies;

namespace SignalRSqlTableDependency.Extension
{
    public static class ApplicationBuilderExtension
    {
        public static async Task UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
            where T : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            await service?.SubscribeTableDependency(connectionString);
        }
    }
}
