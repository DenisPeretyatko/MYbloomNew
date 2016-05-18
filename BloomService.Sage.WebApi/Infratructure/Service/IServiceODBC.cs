namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        void Connection(string connectionString);

        void ConnectionClose();

        List<Dictionary<string, object>> Customers();

        List<Dictionary<string, object>> GetTable(string request, string connectionString);

        List<Dictionary<string, object>> Trucks();

       void UnassignWorkOrder(string id);
}
}
