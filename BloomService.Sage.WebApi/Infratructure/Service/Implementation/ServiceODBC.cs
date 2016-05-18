namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;
    using System.Linq;

    using Sage.WebApi.Infratructure.Constants;

    public class ServiceOdbc : IServiceOdbc
    {
        private readonly string timberlineDataConnectionString;
        private readonly string timberlineServiceManagementConnectionString;

        private ClaimsAgent claimsAgent;

        private bool isOpened;

        private OdbcConnection odbcConnection;

        private DataSet odbcDataSet;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            timberlineDataConnectionString = configConstants.TimberlineDataConnectionString;
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }


        public void Connection(string connectionString)
        {
            odbcDataSet = new DataSet("myData");
            odbcConnection = new OdbcConnection(connectionString);
            odbcConnection.Open();
            isOpened = true;
        }

        public void ConnectionClose()
        {
            odbcConnection.Close();
            isOpened = false;
        }

        public List<Dictionary<string, object>> Customers()
        {
            var connectionString = string.Format("UID={0};pwd={1};DBQ={2}", claimsAgent.Name, claimsAgent.Password, timberlineDataConnectionString);
            return GetTable(Queryes.SelectCustomer, connectionString);
        }

        public List<Dictionary<string, object>> GetTable(string request, string connectionString)
        {
            var claimsAgent = new ClaimsAgent();
            if (!isOpened)
            {
                Connection(connectionString);
            }

            var dataAdapter = new OdbcDataAdapter(request, odbcConnection);
            dataAdapter.Fill(odbcDataSet);
            odbcConnection.Close();
            var table = odbcDataSet.Tables["Table"];
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in table.Rows)
            {
                var row = table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);
                rows.Add(row);
            }

            return rows;
        }

        public void ExecuteScript(string request, string connectionString)
        {
            var claimsAgent = new ClaimsAgent();
            if (!isOpened)
            {
                Connection(connectionString);
            }

            var odbcCommand = odbcConnection.CreateCommand();
            odbcCommand.CommandText = request;
            odbcCommand.ExecuteReader();
        }

        public List<Dictionary<string, object>> Trucks()
        {
            return GetTable(Queryes.SelectTrucks, timberlineDataConnectionString);
        }

        public void UnassignWorkOrder(string id)
        {
            var query = Queryes.GetAssignment.Replace("%ID%", id);
            ExecuteScript(query, timberlineServiceManagementConnectionString);
        }
    }
}