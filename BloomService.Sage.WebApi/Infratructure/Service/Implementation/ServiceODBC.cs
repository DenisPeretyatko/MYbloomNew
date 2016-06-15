namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    using Sage.WebApi.Infratructure.Constants;
    using Utils;
    using AutoMapper;
    using Models.DbModels;
    using System;
    public class ServiceOdbc : IServiceOdbc
    {
        private readonly string timberlineDataConnectionString;

        private readonly string timberlineServiceManagementConnectionString;

        private ClaimsAgent claimsAgent;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            timberlineDataConnectionString = "Driver={Timberline Data};" + string.Format(
                "UID={0};pwd={1};DBQ={2}",
                /*claimsAgent.Name*/ "kris",
                /*claimsAgent.Password*/ "sageDEV!!", configConstants.TimberlineDataConnectionString);
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

        public void UnassignWorkOrder(string id)
        {
            var query = Queries.SelectAssignment.Replace("%ID%", id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public SageWorkOrder EditWorkOrder(SageWorkOrder workOrder)
        {
            var query = Queries.BuildEditWorkOrderQuery(workOrder);
            var properties = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query).SingleOrDefault();
            if (properties != null)
            {
                var result = new SageWorkOrder()
                {
                    ARCustomer = properties.ContainsKey("ARCUST") ? properties["ARCUST"].ToString() : string.Empty,
                    Location = properties.ContainsKey("SERVSITENBR") ? properties["SERVSITENBR"].ToString() : string.Empty,
                    CallType = properties.ContainsKey("CALLTYPECODE") ? properties["CALLTYPECODE"].ToString() : string.Empty,
                    CallDate = properties.ContainsKey("CALLDATE") ? Convert.ToDateTime(properties["CALLDATE"]) : DateTime.MinValue,
                    Problem = properties.ContainsKey("PROBLEMCODE") ? properties["PROBLEMCODE"].ToString() : string.Empty,
                    RateSheet = properties.ContainsKey("RATESHEETNBR") ? properties["RATESHEETNBR"].ToString() : string.Empty,
                    Employee = properties.ContainsKey("TECHNICIAN") ? properties["TECHNICIAN"].ToString() : string.Empty,
                    EstimatedRepairHours = properties.ContainsKey("ESTREPAIRHRS") ? Convert.ToDecimal(properties["ESTREPAIRHRS"]) : Decimal.MinusOne,
                    NottoExceed = properties.ContainsKey("NOTTOEXCEED") ? properties["NOTTOEXCEED"].ToString() : string.Empty,
                    Comments = properties.ContainsKey("COMMENTS") ? properties["COMMENTS"].ToString() : string.Empty,
                    CustomerPO = properties.ContainsKey("CUSTOMERPO") ? properties["CUSTOMERPO"].ToString() : string.Empty,
                    PermissionCode = properties.ContainsKey("PERMISSIONCODE") ? properties["PERMISSIONCODE"].ToString() : string.Empty,
                    PayMethod = properties.ContainsKey("PAYMETHOD") ? properties["PAYMETHOD"].ToString() : string.Empty,
                    Status = properties.ContainsKey("Status") ? properties["Status"].ToString() : string.Empty,

                };

                return result;
            }

            return null;

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

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            using (OdbcCommand command = new OdbcCommand(query, connection))
            using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
            {
                adapter.Fill(dataSet);
            }

            return dataSet;
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