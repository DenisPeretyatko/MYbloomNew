namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        void ConnectionTSM(string connectionString);

        void TdConnectionClose();

        List<Dictionary<string, object>> Customers();

        List<Dictionary<string, object>> GetTable(string request);

        List<Dictionary<string, object>> Trucks();

       void UnassignWorkOrder(string id);
}
}
