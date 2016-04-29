namespace Sage.WebApi.Infratructure.Service
{
    using System.Collections.Generic;

    public interface IServiceOdbc
    {
        void Connection(string name, string password);

        void ConnectionClose();

        List<Dictionary<string, object>> Customers();

        List<Dictionary<string, object>> GetTable(string request);

        List<Dictionary<string, object>> Trucks();
    }
}