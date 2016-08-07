using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace Sage.WebApi.Infratructure.Constants
{
    public static class Queries
    {
        public static readonly string SelectCustomer = "SELECT * FROM ARM_MASTER__CUSTOMER";
        public static readonly string SelectTrucks = "SELECT * FROM JCM_MASTER__JOB WHERE Job LIKE '%T-%'";
        public static readonly string SelectAssignment = "UPDATE ASSIGNS SET EMPLOYEENBR = 0 WHERE WRKORDNBR = %ID%";
        public static readonly string SelectWorkOrders = "SELECT * FROM WRKORDER";
        public static readonly string SelectPermissionCodes = "SELECT * FROM PERMISSIONCODE";
        public static readonly string SelectRateSheets = "SELECT * FROM RATESHEET";
        public static readonly string EditWorkOrderStatus = "UPDATE WRKORDER SET STATUS = {0} WHERE WRKORDNBR = {1}";

        public static string BuildDeleteWorkOrderItemQuery(int workOrderId, IEnumerable<int> ids)
        {
            var query = "DELETE FROM WOITEMS WHERE WRKORDNBR  = {0} and WRKORDITEM in ({1});" 
               + " DELETE FROM WOCOST WHERE WRKORDNBR  = {0} AND WRKORDITEM in ({1});";
            var idString = "{0},";
            var resultIdString = "";
            foreach(var id in ids)
            {
                resultIdString += string.Format(idString, id);
            }
            var result = string.Format(query, workOrderId, resultIdString).Replace(",)", ")");
            return result;
        }

        public static string BuildEditWorkOrderQuery(SageWorkOrder workOrder)
        {
            var query = "UPDATE WRKORDER SET ";

            var parameters = new Dictionary<string, string>();

            parameters.Add("ARCUST", workOrder.ARCustomer);
            parameters.Add("SERVSITENBR", workOrder.Location);
            parameters.Add("CALLTYPECODE", workOrder.CallType);
            if (workOrder.CallDate != null)
            {
                parameters.Add("CALLDATE", ((System.DateTime)workOrder.CallDate).ToString("yyyy-MM-dd"));
            }
            if (workOrder.CallTime != null)
            {
                parameters.Add("CALLTIME", ((DateTime)workOrder.CallTime).ToString("HH:mm:ss") ?? ((DateTime)workOrder.CallTime).ToString("HH:mm:ss"));
            }
            parameters.Add("PROBLEMCODE", workOrder.Problem);
            parameters.Add("RATESHEETNBR", workOrder.RateSheet);
            parameters.Add("TECHNICIAN", workOrder.Employee);
            parameters.Add("ESTREPAIRHRS", workOrder.EstimatedRepairHours.ToString());
            parameters.Add("NOTTOEXCEED", workOrder.NottoExceed);
            parameters.Add("COMMENTS", workOrder.Comments);
            parameters.Add("CUSTOMERPO", workOrder.CustomerPO);
            parameters.Add("PERMISSIONCODE", workOrder.PermissionCode);
            parameters.Add("PAYMETHOD", workOrder.PayMethod);
            if (workOrder.Status == "Open")
            {
                parameters.Add("STATUS", "0");
            }
            if (workOrder.Status == "Closed")
            {
                parameters.Add("STATUS", "3");
            }

            foreach (var parameter in parameters)
            {
                if (parameter.Value != string.Empty && parameter.Value != null)
                {
                    query += string.Format("{0} = '{1}', ", parameter.Key, parameter.Value.Sanitize());
                }
            }

            query += string.Format("WHERE WRKORDNBR = '{0}';", workOrder.WorkOrder);
            query = query.Replace(", WHERE", " WHERE");

            return query;
        }
    }
}