
using Microsoft.AspNetCore.SignalR;
using SignalRSqlTableDependency.Models;
using SignalRSqlTableDependency.SignalRHub;
using TableDependency.SqlClient;

namespace SignalRSqlTableDependency.SubscribeTableDependencies
{
    public class SubscribeProductsTableDependency : ISubscribeTableDependency
    {

        SqlTableDependency<Products>? tableDependency = null;
        private readonly IHubContext<ChatHub, IChatHubClient> _hub;

        public SubscribeProductsTableDependency(IHubContext<ChatHub, IChatHubClient> hub)
        {
            _hub = hub;
        }

        public async Task SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Products>(connectionString, tableName: nameof(Products), schemaName: "dbo");
            tableDependency.OnChanged += async (s, e) =>
            {
                await TableDependency_OnChanged(s, e);
            };
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async Task TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Products> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                //notify to client side
                await _hub.Clients.All.onProductUpdate("Quantity updated.");
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Products)} SqlTableDependency error: {e.Error.Message}");
        }
    }

}
