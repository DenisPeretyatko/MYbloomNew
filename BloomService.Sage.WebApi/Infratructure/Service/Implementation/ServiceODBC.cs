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

        private bool isOpenedTSM;
        private bool isOpenedTD;

        private OdbcConnection tdOdbcConnection;
        private OdbcConnection tsmOdbcConnection;

        private DataSet odbcDataSet;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            timberlineDataConnectionString = configConstants.TimberlineDataConnectionString;
            timberlineServiceManagementConnectionString = configConstants.TimberlineServiceManagementConnectionString;
        }


        public void ConnectionTSM(string connectionString)
        {
            tsmOdbcConnection = new OdbcConnection(connectionString);
            tsmOdbcConnection.Open();
            isOpenedTSM = true;
        }

        public void ConnectionTD(string name, string password)
        {
            var connectionString = string.Format("UID={0};pwd={1};DBQ={2}", name, password, timberlineDataConnectionString);
            odbcDataSet = new DataSet("myData");
            tdOdbcConnection = new OdbcConnection("Driver={Timberline Data};" + connectionString);
            tdOdbcConnection.Open();
            isOpenedTD = true;
        }

        public void TdConnectionClose()
        {
            tdOdbcConnection.Close();
            isOpenedTD = false;
        }

        public void TsmConnectionClose()
        {
            tsmOdbcConnection.Close();
            isOpenedTSM = false;
        }

        public List<Dictionary<string, object>> Customers()
        {
            return GetTable(Queryes.SelectCustomer);
        }

        public List<Dictionary<string, object>> GetTable(string request)
        {
            var claimsAgent = new ClaimsAgent();
            if (!isOpenedTD)
                ConnectionTD(claimsAgent.Name, claimsAgent.Password);
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(request, tdOdbcConnection);
            dataAdapter.Fill(odbcDataSet);
            TdConnectionClose();
            var table = odbcDataSet.Tables["Table"];
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

        public void ExecuteScript(string request, string connectionString)
        {
            var claimsAgent = new ClaimsAgent();
            if (!isOpenedTSM)
            {
                ConnectionTSM(connectionString);
            }

            var odbcCommand = tsmOdbcConnection.CreateCommand();
            odbcCommand.CommandText = request;
            odbcCommand.ExecuteReader();
            TsmConnectionClose();
        }

        public List<Dictionary<string, object>> Trucks()
        {
            return GetTable(Queryes.SelectTrucks);
        }

        public void UnassignWorkOrder(string id)
        {
            var query = Queryes.GetAssignment.Replace("%ID%", id);
            ExecuteScript(query, timberlineServiceManagementConnectionString);
        }
    }
}