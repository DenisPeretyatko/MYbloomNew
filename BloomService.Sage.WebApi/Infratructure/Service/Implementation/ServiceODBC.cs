namespace Sage.WebApi.Infratructure.Service.Implementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Odbc;

    using Sage.WebApi.Infratructure.Constants;

    public class ServiceOdbc : IServiceOdbc
{
        private ClaimsAgent claimsAgent;

        private OdbcConnection odbcConnection;

        private DataSet odbcDataSet;

        private bool isOpened;

        private readonly string dbPath;

        public ServiceOdbc(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            this.claimsAgent = claimsAgent;
            dbPath = configConstants.ConnectionString;
        }

        public void Connection(string name, string password)
        {
            var connectionString = string.Format("UID={0};pwd={1};DBQ={2}", name, password, dbPath);
            odbcDataSet = new DataSet("myData");
            odbcConnection = new OdbcConnection("Driver={Timberline Data};" + connectionString);
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
            return GetTable(Queryes.SelectCustomer);
        }

        public List<Dictionary<string, object>> GetTable(string request)
        {
            var claimsAgent = new ClaimsAgent();
            if (!isOpened)
            {
                Connection(claimsAgent.Name, claimsAgent.Password);
            }

            var dataAdapter = new OdbcDataAdapter(request, odbcConnection);
            dataAdapter.Fill(odbcDataSet);
            odbcConnection.Close();
            var table = odbcDataSet.Tables["Table"];
            var rows = new List<Dictionary<string, object>>();
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

        public List<Dictionary<string, object>> Trucks()
        {
            return GetTable(Queryes.SelectTrucks);
        }
    }
}