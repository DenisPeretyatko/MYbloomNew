namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    using Sage.WebApi.Infratructure.Constants;

    using AutoMapper;

    using System;
    using Common.Logging;

    using Sage.WebApi.Infratructure.Utils;
    using Sage.WebApi.Models;

    public class ServiceOdbc : IServiceOdbc
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceOdbc));

        private readonly string timberlineDataConnectionString;

        private readonly string timberlineServiceManagementConnectionString;

        private ClaimsAgent claimsAgent;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            timberlineDataConnectionString = "Driver={Timberline Data};" + string.Format(
                "UID={0};pwd={1};DBQ={2}",
                claimsAgent.Name,
                claimsAgent.Password, configConstants.TimberlineDataConnectionString);
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }

        public List<SageCustomer> Customers()
        {
            var response = ExecuteQueryAndGetData(timberlineDataConnectionString, Queries.SelectCustomer);
            var result = new List<SageCustomer>();
            response.ForEach(x => result.Add(x.ToObject<SageCustomer>()));
            return result;
        }

        public List<Dictionary<string, object>> Trucks()
        {
            var result = ExecuteQueryAndGetData(timberlineDataConnectionString, Queries.SelectTrucks);
            return result;
        }

        public List<SageRateSheet> RateSheets()
        {
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, Queries.SelectRateSheets);
            var result = new List<SageRateSheet>();
            response.ForEach(x => result.Add(x.ToObject<SageRateSheet>()));
            return result;
        }

        public List<SagePermissionCode> PermissionCodes()
        {
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, Queries.SelectPermissionCodes);
            var result = new List<SagePermissionCode>();
            response.ForEach(x => result.Add(x.ToObject<SagePermissionCode>()));
            return result;
        }

        public void UnassignWorkOrder(string id)
        {
            var query = Queries.SelectAssignment.Replace("%ID%", id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkOrder(SageWorkOrder workOrder)
        {
            var query = Queries.BuildEditWorkOrderQuery(workOrder);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkOrderStatus(string id, string status)
        {
            var query = string.Format(Queries.EditWorkOrderStatus, status, id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public List<SageWorkOrder> WorkOrders()
        {
            var query = Queries.SelectWorkOrders;
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = DictionaryToWorkOrderList(response);
            return result;
        }

        private List<SageWorkOrder> DictionaryToWorkOrderList(List<Dictionary<string, object>> response)
        {
            var result = new List<SageWorkOrder>();
            foreach (var item in response)
            {
                var workOrderDbModel = item.ToObject<WorkOrderDbModel>();
                var workOrder = Mapper.Map<SageWorkOrder>(workOrderDbModel);
                result.Add(workOrder);
            }
            return result;
        }

        public DataSet ExecuteQuery(string connectionString, string query)
        {
            var dataSet = new DataSet();
            _log.InfoFormat("Execute query: {0} connectionString: {1}", query, connectionString);
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            using (OdbcCommand command = new OdbcCommand(query, connection))
            using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
            {
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public void DeleteWorkOrderItems(int workOrderId, IEnumerable<int> ids)
        {
            var query = Queries.BuildDeleteWorkOrderItemQuery(workOrderId, ids);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public List<Dictionary<string, object>> ExecuteQueryAndGetData(string connectionString, string query)
        {
            var table = ExecuteQuery(connectionString, query).Tables["Table"];
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in table.Rows)
            {
                var row = table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);
                rows.Add(row);
            }
            return rows;
        }
    }
}