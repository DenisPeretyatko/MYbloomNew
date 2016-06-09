using System.Collections.Generic;

namespace Sage.WebApi.Infratructure.Constants
{
    public static class Queryes
    {
        public static readonly string SelectCustomer = "SELECT * FROM ARM_MASTER__CUSTOMER";
        public static readonly string SelectTrucks = "SELECT * FROM JCM_MASTER__JOB WHERE Job LIKE '%T-%'";
        public static readonly string SelectAssignment = "UPDATE ASSIGNS SET EMPLOYEENBR = 0 WHERE WRKORDNBR = %ID%";
        public static readonly string SelectWorkOrders = "SELECT * FROM WRKORDER";

        public static string BuildEditWorkOrderQuery(Dictionary<string, string> properties)
        {
            var query = "UPDATE WRKORDER SET ";

            var parameters = new Dictionary<string, string>();

            parameters.Add("ARCUST", properties.ContainsKey("ARCustomer") ? properties["ARCustomer"] : string.Empty);
            parameters.Add("SERVSITENBR", properties.ContainsKey("Location") ? properties["Location"] : string.Empty);
            parameters.Add("CALLTYPECODE", properties.ContainsKey("CallType") ? properties["CallType"] : string.Empty);
            parameters.Add("CALLDATE", properties.ContainsKey("CallDate") ? properties["CallDate"] : string.Empty);
            parameters.Add("CALLTIME", properties.ContainsKey("CallTime") ? properties["CallTime"] : string.Empty);
            parameters.Add("PROBLEMCODE", properties.ContainsKey("Problem") ? properties["Problem"] : string.Empty);
            parameters.Add("RATESHEETNBR", properties.ContainsKey("RateSheet") ? properties["RateSheet"] : string.Empty);
            parameters.Add("TECHNICIAN", properties.ContainsKey("Employee") ? properties["Employee"] : string.Empty);
            parameters.Add("ESTREPAIRHRS", properties.ContainsKey("EstimatedRepairHours") ? properties["EstimatedRepairHours"] : string.Empty);
            parameters.Add("NOTTOEXCEED", properties.ContainsKey("NottoExceed") ? properties["NottoExceed"] : string.Empty);
            parameters.Add("COMMENTS", properties.ContainsKey("Comments") ? properties["Comments"] : string.Empty);
            parameters.Add("CUSTOMERPO", properties.ContainsKey("CustomerPO") ? properties["CustomerPO"] : string.Empty);
            parameters.Add("PERMISSIONCODE", properties.ContainsKey("PermissionCode") ? properties["PermissionCode"] : string.Empty);
            parameters.Add("PAYMETHOD", properties.ContainsKey("PayMethod") ? properties["PayMethod"] : string.Empty);

            foreach (var parameter in parameters)
            {
                if (parameter.Value != string.Empty)
                {
                    query += string.Format("{0} = '{1}', ", parameter.Key, parameter.Value);
                }
            }

            query += string.Format("WHERE WRKORDNBR = '{0}';", properties["WorkOrder"]);
            query = query.Replace(", WHERE", " WHERE");
            query += string.Format("SELECT * FROM WRKORDER WHERE WRKORDNBR= '{0}';", properties["WorkOrder"]);
            return query;
        }

        public static string BuildCreateWorkOrderQuery(Dictionary<string, string> properties)
        {
            var query = "INSERT INTO WRKORDER (";

            var parameters = new Dictionary<string, string>();

            parameters.Add("ARCUST", properties.ContainsKey("ARCustomer") ? properties["ARCustomer"] : string.Empty);
            parameters.Add("SERVSITENBR", properties.ContainsKey("Location") ? properties["Location"] : string.Empty);
            parameters.Add("CALLTYPECODE", properties.ContainsKey("CallType") ? properties["CallType"] : string.Empty);
            parameters.Add("CALLDATE", properties.ContainsKey("CallDate") ? properties["CallDate"] : string.Empty);
            parameters.Add("CALLTIME", properties.ContainsKey("CallTime") ? properties["CallTime"] : string.Empty);
            parameters.Add("PROBLEMCODE", properties.ContainsKey("Problem") ? properties["Problem"] : string.Empty);
            parameters.Add("RATESHEETNBR", properties.ContainsKey("RateSheet") ? properties["RateSheet"] : string.Empty);
            parameters.Add("TECHNICIAN", properties.ContainsKey("Employee") ? properties["Employee"] : string.Empty);
            parameters.Add("ESTREPAIRHRS", properties.ContainsKey("EstimatedRepairHours") ? properties["EstimatedRepairHours"] : string.Empty);
            parameters.Add("NOTTOEXCEED", properties.ContainsKey("NottoExceed") ? properties["NottoExceed"] : string.Empty);
            parameters.Add("COMMENTS", properties.ContainsKey("Comments") ? properties["Comments"] : string.Empty);
            parameters.Add("CUSTOMERPO", properties.ContainsKey("CustomerPO") ? properties["CustomerPO"] : string.Empty);
            parameters.Add("PERMISSIONCODE", properties.ContainsKey("PermissionCode") ? properties["PermissionCode"] : string.Empty);
            parameters.Add("PAYMETHOD", properties.ContainsKey("PayMethod") ? properties["PayMethod"] : string.Empty);

            foreach (var parameter in parameters)
            {
                if (parameter.Value == string.Empty)
                {
                    parameters.Remove(parameter.Key);
                }
            }

            foreach (var parameter in parameters)
            {
                query += parameter.Key + ",";
            }

            query += ") VALUES (";

            foreach (var parameter in parameters)
            {
                query += parameter.Value + ",";
            }

            query.Replace(",)", ")");

            query += string.Format("WHERE WRKORDNBR = '{0}';", properties["WorkOrder"]);
            query = query.Replace(", WHERE", " WHERE");
            return query;
        }
    }
}