using BloomService.Domain.Entities.Concrete;
using System.Collections.Generic;

namespace Sage.WebApi.Infratructure.Constants
{
    public static class Queries
    {
        public static readonly string SelectCustomer = "SELECT * FROM ARM_MASTER__CUSTOMER";
        public static readonly string SelectTrucks = "SELECT * FROM JCM_MASTER__JOB WHERE Job LIKE '%T-%'";
        public static readonly string SelectAssignment = "UPDATE ASSIGNS SET EMPLOYEENBR = 0 WHERE WRKORDNBR = %ID%";
        public static readonly string SelectWorkOrders = "SELECT * FROM WRKORDER";

        public static string BuildEditWorkOrderQuery(SageWorkOrder workOrder)
        {
            var query = "UPDATE WRKORDER SET ";

            var parameters = new Dictionary<string, string>();

            parameters.Add("ARCUST", workOrder.ARCustomer);
            parameters.Add("SERVSITENBR", workOrder.Location);
            parameters.Add("CALLTYPECODE", workOrder.CallType);
            parameters.Add("CALLDATE", workOrder.CallDate.ToString("yyyy-MM-dd"));
            parameters.Add("CALLTIME", workOrder.CallTime.ToString());
            parameters.Add("PROBLEMCODE", workOrder.Problem);
            parameters.Add("RATESHEETNBR", workOrder.RateSheet);
            parameters.Add("TECHNICIAN", workOrder.Employee);
            parameters.Add("ESTREPAIRHRS", workOrder.EstimatedRepairHours.ToString());
            parameters.Add("NOTTOEXCEED", workOrder.NottoExceed);
            parameters.Add("COMMENTS", workOrder.Comments);
            parameters.Add("CUSTOMERPO", workOrder.CustomerPO);
            parameters.Add("PERMISSIONCODE", workOrder.PermissionCode);
            parameters.Add("PAYMETHOD", workOrder.PayMethod);


            foreach (var parameter in parameters)
            {
                if (parameter.Value != string.Empty && parameter.Value != null)
                {
                    query += string.Format("{0} = '{1}', ", parameter.Key, parameter.Value);
                }
            }

            query += string.Format("WHERE WRKORDNBR = '{0}';", workOrder.WorkOrder);
            query = query.Replace(", WHERE", " WHERE");
            query += string.Format(" SELECT * FROM WRKORDER WHERE WRKORDNBR= '{0}';", workOrder.WorkOrder);
            return query;
        }
    }
}