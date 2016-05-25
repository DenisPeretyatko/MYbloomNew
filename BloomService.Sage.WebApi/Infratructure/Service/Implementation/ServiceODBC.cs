namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Linq;

    using BloomService.Domain.Entities.Concrete;

    using Sage.WebApi.Infratructure.Constants;

    public class ServiceOdbc : IServiceOdbc
    {
        private readonly string timberlineDataConnectionString;

        private readonly string timberlineServiceManagementConnectionString;

        private ClaimsAgent claimsAgent;

        private bool isOpenedTd;

        private bool isOpenedTsm;

        private DataSet odbcDataSet;

        private OdbcConnection tdOdbcConnection;

        private OdbcConnection tsmOdbcConnection;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            timberlineDataConnectionString = configConstants.TimberlineDataConnectionString;
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }

        public void ConnectionTd(string name, string password)
        {
            var connectionString = string.Format(
                "UID={0};pwd={1};DBQ={2}", 
                name, 
                password, 
                timberlineDataConnectionString);
            odbcDataSet = new DataSet("myData");
            tdOdbcConnection = new OdbcConnection("Driver={Timberline Data};" + connectionString);
            tdOdbcConnection.Open();
            isOpenedTd = true;
        }

        public void ConnectionTsm(string connectionString)
        {
            tsmOdbcConnection = new OdbcConnection(connectionString);
            tsmOdbcConnection.Open();
            isOpenedTsm = true;
        }

        public List<Dictionary<string, object>> Customers()
        {
            return GetTable(Queryes.SelectCustomer);
        }

        public void ExecuteScript(string request, string connectionString)
        {
            if (!isOpenedTsm)
            {
                ConnectionTsm(connectionString);
            }

            var odbcCommand = tsmOdbcConnection.CreateCommand();
            odbcCommand.CommandText = request;
            odbcCommand.ExecuteReader();
            TsmConnectionClose();
        }

        public List<Dictionary<string, object>> GetTable(string request)
        {
            claimsAgent = new ClaimsAgent();
            if (!isOpenedTd)
            {
                ConnectionTd(claimsAgent.Name, claimsAgent.Password);
            }

            var dataAdapter = new OdbcDataAdapter(request, tdOdbcConnection);
            dataAdapter.Fill(odbcDataSet);
            TdConnectionClose();
            var table = odbcDataSet.Tables["Table"];
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in table.Rows)
            {
                var row = table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);

                rows.Add(row);
            }

            return rows;
        }

        public void TdConnectionClose()
        {
            tdOdbcConnection.Close();
            isOpenedTd = false;
        }

        public List<Dictionary<string, object>> Trucks()
        {
            return GetTable(Queryes.SelectTrucks);
        }

        public void TsmConnectionClose()
        {
            tsmOdbcConnection.Close();
            isOpenedTsm = false;
        }

        public void UnassignWorkOrder(string id)
        {
            var query = Queryes.GetAssignment.Replace("%ID%", id);
            ExecuteScript(query, timberlineServiceManagementConnectionString);
        }

        public void EditWorkOrder(SageWorkOrder workOrder)
        {
            var query = Queryes.GetAssignment;
            ExecuteScript(query, timberlineServiceManagementConnectionString);
        }

        //public void EditWorkOrder(SageWorkOrder workOrder)
        //{
        //    var query = Queryes.GetAssignment.Replace("%ID%", id);
        //    ExecuteScript(query, timberlineServiceManagementConnectionString);
        //}
    }
}