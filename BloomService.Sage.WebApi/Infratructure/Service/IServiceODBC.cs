using System.Collections.Generic;

namespace Sage.WebApi.Infratructure.Service
{
    public interface IServiceODBC
    {
        void Connection(string name, string password);
        void ConnectionClose();
        List<Dictionary<string, object>> GetTable(string request);
        List<Dictionary<string, object>> Customers();
        List<Dictionary<string, object>> Trucks();
    }
}
