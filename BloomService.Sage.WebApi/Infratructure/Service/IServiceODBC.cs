namespace Sage.WebApi.Infratructure.Service
{
    using BloomService.Domain.Entities.Concrete;
    using System.Collections.Generic;

    public interface IServiceOdbc
{
        void ConnectionTsm(string connectionString);

        void TdConnectionClose();

        List<Dictionary<string, object>> Customers();

        List<Dictionary<string, object>> GetTable(string request);

        List<Dictionary<string, object>> Trucks();

        void UnassignWorkOrder(string id);

        void EditWorkOrder(SageWorkOrder workOrder);
       
    }
}
