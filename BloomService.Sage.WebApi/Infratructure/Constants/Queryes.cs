namespace Sage.WebApi.Infratructure.Constants
{
    public static class Queryes
    {
        public static readonly string SelectCustomer = "SELECT * FROM ARM_MASTER__CUSTOMER";
        public static readonly string SelectTrucks = "SELECT * FROM JCM_MASTER__JOB WHERE Job LIKE '%T-%'";
        public static readonly string GetAssignment = "UPDATE ASSIGNS SET EMPLOYEENBR =0 WHERE WRKORDNBR = %ID%";
        public static readonly string EditWorkOrder = "UPDATE ASSIGNS SET EMPLOYEENBR =0 WHERE WRKORDNBR = %ID%";
    }
}