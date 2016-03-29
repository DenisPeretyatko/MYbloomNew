using Sage.WebApi.Infratructure.Constants;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace Sage.WebApi.Infratructure.Service.Implementation
{
    public class ServiceODBC: IServiceODBC
    {
        public bool Opened;
        public DataSet OdbcDataSet;
        public OdbcConnection OdbcConnection;
        public string PathToDB;
        public ClaimsAgent ClaimsAgent;

        public ServiceODBC(ClaimsAgent claimsAgent, SageWebConfig configConstants)
        {
            ClaimsAgent = claimsAgent;
            PathToDB = configConstants.ConnectionString;
        }

        public void Connection(string name, string password)
        {
            var connectionString = string.Format("UID={0};pwd={1};DBQ={2}", name, password, PathToDB);
            OdbcDataSet = new DataSet("myData");
            OdbcConnection = new OdbcConnection("Driver={Timberline Data};" + connectionString);
            OdbcConnection.Open();
            Opened = true;
        }

        public List<Dictionary<string, object>> Customers()
        {
            return GetTable(Queryes.SelectCustomer);
        }

        public List<Dictionary<string, object>> Trucks()
        {
            return GetTable(Queryes.SelectTrucks);
        }

        public void ConnectionClose()
        {
            OdbcConnection.Close();
            Opened = false;
        }

        public List<Dictionary<string, object>> GetTable(string request)
        {
            var claimsAgent = new ClaimsAgent();
            if (!Opened)
                Connection(claimsAgent.Name, claimsAgent.Password);
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(request, OdbcConnection);
            dataAdapter.Fill(OdbcDataSet);
            OdbcConnection.Close();
            var table = OdbcDataSet.Tables["Table"];
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
    }
}