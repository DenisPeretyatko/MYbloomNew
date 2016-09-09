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
                configConstants.SageUserName,
                configConstants.SagePassword, configConstants.TimberlineDataConnectionString);
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }

        public List<SageCustomer> Customers()
        {
            var response = ExecuteQueryAndGetData(timberlineDataConnectionString, Queries.SelectCustomerQuery);
            var result = new List<SageCustomer>();
            response.ForEach(x => result.Add(x.ToObject<SageCustomer>()));
            return result;
        }

        public List<SageRateSheet> RateSheets()
        {
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, Queries.SelectRateSheetsQuery);
            var result = new List<SageRateSheet>();
            response.ForEach(x => result.Add(x.ToObject<SageRateSheet>()));
            return result;
        }

        public List<SageWorkOrderLocationAccordance> GetWorkOrderLocationAccordance()
        {
            var query = Queries.SelectWorkOrderLocationAccordanceQuery;
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = new List<SageWorkOrderLocationAccordance>();
            response.ForEach(x => result.Add(x.ToObject<SageWorkOrderLocationAccordance>()));
            return result;
        }

        public List<SagePermissionCode> PermissionCodes()
        {
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, Queries.SelectPermissionCodesQuery);
            var result = new List<SagePermissionCode>();
            response.ForEach(x => result.Add(x.ToObject<SagePermissionCode>()));
            return result;
        }

        public void UnassignWorkOrder(string id)
        {
            var query = string.Format(Queries.UpdateAssignmentQuery, id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkOrder(SageWorkOrder workOrder)
        {
            var query = Queries.BuildEditWorkOrderQuery(workOrder);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkOrderStatus(string id, string status)
        {
            var query = string.Format(Queries.EditWorkOrderStatusQuery, status, id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkJcJob(string id, string jcjob)
        {
            var query = string.Format(Queries.EditWorkJcJobQuery, jcjob, id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void AddNote(SageNote note)
        {
            var query = Queries.BuildAddNoteQuery(note);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditNote(SageNote note)
        {
            var query = Queries.BuildEditNoteQuery(note);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void DeleteNote(string id)
        {
            var query = string.Format(Queries.DeleteNoteQuery, id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void DeleteNotes(IEnumerable<long> ids)
        {
            var query = string.Format(Queries.BuildDeleteWorkOrderNotesQuery(ids));
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public List<SageNote> GetNotes(string id)
        {
            var query = string.Format(Queries.SelectNotesQuery, id);
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = new List<SageNote>();
            response.ForEach(x => result.Add(x.ToObject<SageNote>()));
            return result;
        }

        public List<SageNote> GetNotes()
        {
            var query = Queries.SelectAllNotesQuery;
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = new List<SageNote>();
            response.ForEach(x => result.Add(x.ToObject<SageNote>()));
            return result;
        }

        public List<SageWorkOrderItem> GetWorkOrderItems()
        {
            var query = Queries.SelectAllWorkOrderItemsQuery;
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = DictionaryToWorkOrderItemList(response);
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

        private List<SageWorkOrderItem> DictionaryToWorkOrderItemList(List<Dictionary<string, object>> response)
        {
            var result = new List<SageWorkOrderItem>();
            foreach (var item in response)
            {
                var workOrderItemDbModel = item.ToObject<WorkOrderItemDbModel>();
                var workOrder = Mapper.Map<SageWorkOrderItem>(workOrderItemDbModel);
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