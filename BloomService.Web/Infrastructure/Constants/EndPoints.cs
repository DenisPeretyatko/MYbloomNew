namespace BloomService.Web.Infrastructure.Constants
{
    public static class EndPoints
    {
        private static string endPointBase = "api/v2/sm/";

        public static string AddWorkOrder = endPointBase + "workorders/add";
        public static string AddAssignment = endPointBase + "assignments/add";
        public static string GetAssignments = endPointBase + "assignments/get";
        public static string GetCalltypes = endPointBase + "calltypes/get";
        public static string GetDepartments = endPointBase + "departments/get";
        public static string EditAssignment = endPointBase + "assignments/edit";
        public static string EditNotification = endPointBase + "notifications/edit";
        public static string EditWorkOrder = endPointBase + "workorders/edit";
        public static string GetEmployees = endPointBase + "employees/get";
        public static string GetEquipment = endPointBase + "equipment/get";
        public static string GetLocations = endPointBase + "location/get";
        public static string GetParts = endPointBase + "parts/get";
        public static string GetPermissionCodes = endPointBase + "permissioncodes/get";
        public static string GetProblems = endPointBase + "problems/get";
        public static string GetRateSheets = endPointBase + "ratesheets/get";
        public static string GetRepairs = endPointBase + "repairs/get";
        public static string UnassignWorkOrders = endPointBase + "workorders/unassign";
        public static string GetWorkorder = endPointBase + "workorders/get";
        public static string GetCustomer = "api/v2/ar/customers";
        public static string AddWorkOrderItem = endPointBase + "workorders/equipment/add";
        public static string EditWorkOrderItem = endPointBase + "workorders/equipment/edit";
        public static string Authorization = "api/v2/Authorization/Authorization";
        public static string GetWorkOrderItemsByWorkorderId = endPointBase + "workorder/{id}/equipments";
        public static string DeleteWorkOrderItems = endPointBase + "workorders/workorderitems/delete";
        public static string EditWorkOrderStatus = endPointBase + "workorders/status/edit";

        public static string CreateNote = endPointBase + "workorders/notes/create";
        public static string EditNote = endPointBase + "workorders/notes/edit";
        public static string DeleteNote = endPointBase + "workorders/notes/remove";
    }
}