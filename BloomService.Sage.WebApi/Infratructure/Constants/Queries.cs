using BloomService.Domain.Entities.Concrete;
using BloomService.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace Sage.WebApi.Infratructure.Constants
{
    public static class Queries
    {
        public static readonly string SelectCustomerQuery = "SELECT * FROM ARM_MASTER__CUSTOMER";
        public static readonly string SelectTrucksQuery = "SELECT * FROM JCM_MASTER__JOB WHERE Job LIKE '%T-%'";
        public static readonly string UpdateAssignmentQuery = "UPDATE ASSIGNS SET EMPLOYEENBR = 0 WHERE WRKORDNBR = {0}";
        public static readonly string SelectPermissionCodesQuery = "SELECT * FROM PERMISSIONCODE";
        public static readonly string SelectRateSheetsQuery = "SELECT * FROM RATESHEET";
        public static readonly string EditWorkOrderStatusQuery = "UPDATE WRKORDER SET STATUS = {0} WHERE WRKORDNBR = {1}";
        public static readonly string EditWorkJcJobQuery = "UPDATE WRKORDER SET JCJOB = {0} WHERE WRKORDNBR = {1}";
        public static readonly string DeleteNoteQuery = "DELETE FROM NOTES WHERE NOTENBR = {0}";
        public static readonly string SelectNotesQuery = "SELECT * FROM NOTES WHERE TRANSNBR = {0}";
        public static readonly string SelectAllNotesQuery = "SELECT * FROM NOTES";
        public static readonly string SelectAllWorkOrderItemsQuery = "SELECT * FROM WOITEMS";
        public static readonly string SelectWorkOrderLocationAccordanceQuery = "SELECT WRKORDNBR, SERVSITENBR FROM WRKORDER";
        public static readonly string MarkWorkOrderAsReviewed = "UPDATE \"WRKORDER\" SET STATUS=7 WHERE WRKORDNBR = {0}";

        public static string BuildDeleteWorkOrderItemQuery(int workOrderId, IEnumerable<int> ids)
        {
            var query = "DELETE FROM WOITEMS WHERE WRKORDNBR = {0} and WRKORDITEM in ({1});" 
               + " DELETE FROM WOCOST WHERE WRKORDNBR = {0} AND WRKORDITEM in ({1});";
            var idString = "{0},";
            var resultIdString = "";
            foreach(var id in ids)
            {
                resultIdString += string.Format(idString, id);
            }
            var result = string.Format(query, workOrderId, resultIdString).Replace(",)", ")");
            return result;
        }

        public static string BuildDeleteWorkOrderNotesQuery(IEnumerable<long> ids)
        {
            var query = "DELETE FROM NOTES WHERE NOTENBR in ({0});";
            var idString = "{0},";
            var resultIdString = "";
            foreach (var id in ids)
            {
                resultIdString += string.Format(idString, id);
            }
            var result = string.Format(query, resultIdString).Replace(",)", ")");
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
                parameters.Add("CALLDATE", ((DateTime)workOrder.CallDate).ToString("yyyy-MM-dd"));
            }
            if (workOrder.CallTime != null)
            {
                parameters.Add("CALLTIME", ((DateTime)workOrder.CallTime).ToString("HH:mm:ss") 
                    ?? ((DateTime)workOrder.CallTime).ToString("HH:mm:ss"));
            }
            parameters.Add("PROBLEMCODE", workOrder.Problem);
            parameters.Add("RATESHEETNBR", workOrder.RateSheet);
            parameters.Add("TECHNICIAN", workOrder.Employee);
            parameters.Add("CONTACT", workOrder.Contact);
            parameters.Add("SYSEQPNBR", workOrder.Equipment.ToString());

            if (workOrder.EstimatedRepairHours != 0)
            {
                parameters.Add("ESTREPAIRHRS", workOrder.EstimatedRepairHours.ToString());
            }
            parameters.Add("NOTTOEXCEED", workOrder.NottoExceed);
            parameters.Add("COMMENTS", workOrder.Comments);
            parameters.Add("CUSTOMERPO", workOrder.CustomerPO);
            parameters.Add("PERMISSIONCODE", workOrder.PermissionCode);
            parameters.Add("PAYMETHOD", workOrder.PayMethod);
            parameters.Add("JCJOB", workOrder.JCJob);

            switch (workOrder.Status) {
                case "Open":
                    parameters.Add("STATUS", "0");
                    break;
                case "Closed":
                    parameters.Add("STATUS", "3");
                    break;
                case "Cancelled":
                    parameters.Add("STATUS", "4");
                    break;
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

        internal static string BuildAddNoteQuery(SageNote note)
        {
            var query = "INSERT INTO NOTES (TYPE, TRANSNBR, TRANSNBR2, TRANSNBR3, "
                + "DATE, EMPLOYEENBR, DATEENTER, FORMAT, DATABASENBR, NOTENBR, QSYSGEN, QCUSTVIEW, SPARE, SUBJECTLINE, TEXT) "
                + $"VALUES ('{ note.TYPE}',"
                + $"'{ note.TRANSNBR }',"
                + $"'{ note.TRANSNBR2 }',"
                + $"'{ note.TRANSNBR3 }',"
                + $"'{ ((DateTime)note.DATE).ToString("yyyy-MM-dd") }',"
                + $"'{ note.EMPLOYEENBR }',"
                + $"'{ ((DateTime)note.DATEENTER).ToString("yyyy-MM-dd") }',"
                + $"'{ note.FORMAT }',"
                + $"'{ note.DATABASENBR }',"
                + $"'{ note.NOTENBR }',"
                + $"'{ note.QSYSGEN }',"
                + $"'{ note.QCUSTVIEW }',"
                + $"'{ note.SPARE }',"
                + $"'{ note.SUBJECTLINE }',"
                + $"'{ note.TEXT }');";

            return query;
        }

        internal static string BuildEditNoteQuery(SageNote note)
        {
            var query = "UPDATE NOTES SET ";

            var parameters = new Dictionary<string, string>();

            parameters.Add("TYPE", note.TYPE.ToString());
            parameters.Add("TRANSNBR", note.TRANSNBR.ToString());
            parameters.Add("TRANSNBR2", note.TRANSNBR2.ToString());
            parameters.Add("TRANSNBR3", note.TRANSNBR3.ToString());

            if (note.DATE != null)
            {
                parameters.Add("DATE", ((DateTime)note.DATE).ToString("yyyy-MM-dd"));
            }

            parameters.Add("EMPLOYEENBR", note.EMPLOYEENBR.ToString());

            if (note.DATEENTER != null)
            {
                parameters.Add("DATEENTER", ((DateTime)note.DATEENTER).ToString("yyyy-MM-dd"));   
            }

            parameters.Add("FORMAT", note.FORMAT.ToString());
            parameters.Add("DATABASENBR", note.DATABASENBR.ToString());
            parameters.Add("QSYSGEN", note.QSYSGEN);
            parameters.Add("QCUSTVIEW", note.QCUSTVIEW); 
            parameters.Add("SPARE", note.SPARE);
            parameters.Add("SUBJECTLINE", note.SUBJECTLINE);
            parameters.Add("TEXT", note.TEXT);

            foreach (var parameter in parameters)
            {
                if (parameter.Value != string.Empty && parameter.Value != null)
                {
                    query += string.Format("{0} = '{1}', ", parameter.Key, parameter.Value.Sanitize());
                }
            }

            query += string.Format("WHERE NOTENBR = '{0}';", note.NOTENBR.ToString());
            query = query.Replace(", WHERE", " WHERE");
        
            return query;
        }
    }
}