﻿namespace Sage.WebApi.Infratructure.Service.Implementation
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
                claimsAgent.Name,
                claimsAgent.Password, configConstants.TimberlineDataConnectionString);
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }

        public List<Dictionary<string, object>> Customers()
        {
            return ExecuteQueryAndGetData(timberlineDataConnectionString, Queryes.SelectCustomer);
        }

        public List<Dictionary<string, object>> Trucks()
        {
            //return ExecuteQueryAndGetData(timberlineDataConnectionString, Queryes.SelectTrucks);
            var query = Queryes.SelectWorkOrders;
            var result = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            return result;
        }

        public void UnassignWorkOrder(string id)
        {
            var query = Queryes.SelectAssignment.Replace("%ID%", id);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }

        public void EditWorkOrder(SageWorkOrder workOrder)
        {
            var properties = SagePropertyConverter.ConvertToProperties(workOrder);
            var query = Queryes.BuildEditWorkOrderQuery(properties);
            ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
        }

        public void CreateWorkOrder(SageWorkOrder workOrder)
        {
            var properties = SagePropertyConverter.ConvertToProperties(workOrder);
            var query = Queryes.BuildCreateWorkOrderQuery(properties);
            ExecuteQuery(timberlineServiceManagementConnectionString, query);
        }


        public List<SageWorkOrder> WorkOrders()
        {
            var query = Queryes.SelectWorkOrders;
            var response = ExecuteQueryAndGetData(timberlineServiceManagementConnectionString, query);
            var result = DictionaryToWorkOrderList(response);
            return result;
        }

        private List<SageWorkOrder> DictionaryToWorkOrderList(List<Dictionary<string, object>> response)
        {
            var result = new List<SageWorkOrder>();
            foreach(var item in response)
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