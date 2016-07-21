namespace Sage.WebApi.Infratructure.Constants
{
    public static class Messages
    {
        public static readonly string MessageTypeDescriptor = "<MessageTypeDescriptor MessageId = 'SMCommCenterRequest' MustHaveTarget = 'true' ><DataConnection URL = 'C:\\STOData\\Timberline\\Bloom Roofing Reconstructed' /><LibraryLicense Name = 'SMCommCenter' ID = 'C7FC4D2C-BBC3-4a0b-B58A-ED2A9CBDF07F'/><Security UID = '{0}' Password = '{1}' /></MessageTypeDescriptor >";
        public static readonly string Locations = "<Message><Get Entity='Location'><AllProperties/></Get></Message>";
        public static readonly string Parts = "<Message><Get Entity='Part'><AllProperties/></Get></Message>";
        public static readonly string Problems = "<Message><Get Entity='Problem'><AllProperties/></Get></Message>";
        public static readonly string Repairs = "<Message><Get Entity='Repair'><AllProperties/></Get></Message>";
        public static readonly string Employees = "<Message><Get Entity='Employee'><AllProperties/></Get></Message>";
        public static readonly string WorkOrders = "<Message><Get Entity='Work Order'><AllProperties/></Get></Message>";
        public static readonly string WorkOrdersValue = "<Message><Get Entity='Work Order'><Filter><Properties><Property Name = 'WorkOrder' Value = '{0}'/></Properties></Filter><AllProperties/></Get></Message>";
        public static readonly string Calltypes = "<Message><Get Entity='Call Type'><AllProperties/></Get></Message>";
        public static readonly string Departments = "<Message><Get Entity='Department'><AllProperties/></Get></Message>";
        public static readonly string Equipments = "<Message><Get Entity='Equipment'><AllProperties/></Get></Message>";
        public static readonly string Agreements = "<Message><Get Entity='Agreement'><AllProperties/></Get></Message>";
        public static readonly string CreateWorkOrder = "<Message><CreateWorkOrder><Properties>{0}</Properties></CreateWorkOrder></Message>";
        public static readonly string Property = "<Property Name='{0}' Value='{1}'/>";
        public static readonly string Assignments = "<Message><Get Entity='Assignment'><AllProperties/></Get></Message>";
        public static readonly string AssignmentsValue = "<Message><Get Entity='Assignment'><Filter><Properties><Property Name = 'Assignment' Value = '{0}'/></Properties></Filter><AllProperties/></Get></Message>";
        public static readonly string AddAssignment = "<Message><AddAssignment><Properties>{0}</Properties></AddAssignment></Message>";
        public static readonly string EditAssignment = "<Message><EditAssignment><Properties>{0}</Properties></EditAssignment></Message>";
        public static readonly string RateSheet = "<Message><Get Entity='RateSheet'><AllProperties/></Get></Message>";
        public static readonly string AddWorkOrderItem = "<Message><AddWorkOrderItem><Properties>{0}</Properties></AddWorkOrderItem></Message>";
        public static readonly string GetEquipmentsByWorkOrderId = "<Message><Get Entity='Work Order Item'><Filter><Properties><Property Name='WorkOrder' Value='{0}'/></Properties></Filter><AllProperties/></Get></Message>";
        public static readonly string GetAssignmentByWorkOrderId = "<Message><Get Entity='Assignment'><Filter><Properties><Property Name='WorkOrder' Value='{0}'/></Properties></Filter><AllProperties/></Get></Message>";
        public static readonly string EditWorkOrderItem = "<Message><EditWorkOrderItem><Properties>{0}</Properties></EditWorkOrderItem></Message>";
        public static readonly string UnAssignWorkOrder = "<Message><EditAssignment><Properties><Property Name='Employee' Value='0' /><Property Name='WorkOrder' Value = '{0}' /></Properties></EditAssignment></Message>";
    }
}