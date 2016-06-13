using System;
using System.Collections.Specialized;

namespace BloomService.Domain.Extensions
{
    public class BloomServiceConfiguration
    {
        public static BloomServiceConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                //Path mapped from URL
                var configuration = new BloomServiceConfiguration
                {
                    SageUsername = nameValueCollection["SageUsername"],
                    SagePassword = nameValueCollection["SagePassword"],
                    EndPointBase = nameValueCollection["EndPointBase"],
                    WorkOrderEndPoint = nameValueCollection["WorkOrderEndPoint"],
                    EmployeeEndPoint = nameValueCollection["EmployeeEndPoint"],
                    LocationEndPoint = nameValueCollection["LocationEndPoint"],
                    ProblemEndPoint = nameValueCollection["ProblemEndPoint"],
                    CallTypeEndPoint = nameValueCollection["CallTypeEndPoint"],
                    PartEndPoint = nameValueCollection["PartEndPoint"],
                    RepairEndPoint = nameValueCollection["RepairEndPoint"],
                    EquipmentEndPoint = nameValueCollection["EquipmentEndPoint"],
                    DepartmentEndPoint = nameValueCollection["DepartmentEndPoint"],
                    AssignmentEndPoint = nameValueCollection["AssignmentEndPoint"],
                    CustomerEndPoint = nameValueCollection["CustomerEndPoint"],
                    SyncDb = nameValueCollection["SyncDb"],
                    SageApiHost = nameValueCollection["SageApiHost"],
                    BSUrl = nameValueCollection["BSUrl"],
                    SertificateUrl = nameValueCollection["SertificateUrl"],
                    SizeBigPhoto = int.Parse(nameValueCollection["SizeBigPhoto"]),
                    SizeSmallPhoto = int.Parse(nameValueCollection["SizeSmallPhoto"])
                };

                return configuration;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }

        public string SageUsername { get; set; }
        public string SagePassword { get; set; }
        public string EndPointBase { get; set; }
        public string WorkOrderEndPoint { get; set; }
        public string EmployeeEndPoint { get; set; }
        public string LocationEndPoint { get; set; }
        public string ProblemEndPoint { get; set; }
        public string CallTypeEndPoint { get; set; }
        public string PartEndPoint { get; set; }
        public string RepairEndPoint { get; set; }
        public string EquipmentEndPoint { get; set; }
        public string DepartmentEndPoint { get; set; }
        public string AssignmentEndPoint { get; set; }
        public string CustomerEndPoint { get; set; }
        public string SyncDb { get; set; }
        public string SageApiHost { get; set; }
        public string BSUrl { get; set; }
        public string SertificateUrl { get; set; }
        public int SizeBigPhoto { get; set; }
        public int SizeSmallPhoto { get; set; }
    }
}
