﻿namespace BloomService.Web.Infrastructure
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class BloomServiceConfiguration
    {
        public bool AlertBadgeNotificationEnabled { get; set; }

        public bool AlertBadgeSoundNotificationEnabled { get; set; }

        public bool AlertNotificationEnabled { get; set; }

        public string AssignmentEndPoint { get; set; }

        public string CallTypeEndPoint { get; set; }

        public int CheckTechniciansDelay { get; set; }

        public string Connection { get; set; }

        public string CurrentTimezone { get; set; }

        public string CustomerEndPoint { get; set; }

        public string DbName { get; set; }

        public string DepartmentEndPoint { get; set; }

        public string EmployeeEndPoint { get; set; }

        public string EndPointBase { get; set; }

        public string EquipmentEndPoint { get; set; }

        public string LocationEndPoint { get; set; }

        public string NotificationAlert { get; set; }

        public int NotificationBadge { get; set; }

        public int NotificationDelay { get; set; }

        public string NotificationSound { get; set; }

        public string PartEndPoint { get; set; }

        public string ProblemEndPoint { get; set; }

        public bool PureNotificationEnabled { get; set; }

        public string RepairEndPoint { get; set; }

        public string SageApiHost { get; set; }

        public string SagePassword { get; set; }

        public string SageUsername { get; set; }

        public string SertificateUrl { get; set; }

        public string SiteUrl { get; set; }

        public int SizeBigPhoto { get; set; }

        public int SizeSmallPhoto { get; set; }

        public string SyncDb { get; set; }

        public int SynchronizationDelay { get; set; }

        public string UrlToFolderPhotoWork { get; set; }

        public string UrlToFolderPhotoWorkOrders { get; set; }

        public string UrlToFolderTecnician { get; set; }

        public string UrlToFolderWorkOrder { get; set; }

        public string UrlToTechnicianIcon { get; set; }

        public string UrlToWorkOrderIcon { get; set; }

        public string WorkOrderEndPoint { get; set; }

        public string StorageUrl { get; set; }

        public static BloomServiceConfiguration FromWebConfig(NameValueCollection nameValueCollection)
        {
            try
            {
                // Path mapped from URL
                var configuration = new BloomServiceConfiguration
                                        {
                                            SageUsername = nameValueCollection["SageUsername"], 
                                            SagePassword = nameValueCollection["SagePassword"], 
                                            EndPointBase = nameValueCollection["EndPointBase"], 
                                            WorkOrderEndPoint =
                                                nameValueCollection["WorkOrderEndPoint"], 
                                            EmployeeEndPoint =
                                                nameValueCollection["EmployeeEndPoint"], 
                                            LocationEndPoint =
                                                nameValueCollection["LocationEndPoint"], 
                                            ProblemEndPoint =
                                                nameValueCollection["ProblemEndPoint"], 
                                            CallTypeEndPoint =
                                                nameValueCollection["CallTypeEndPoint"], 
                                            PartEndPoint = nameValueCollection["PartEndPoint"], 
                                            RepairEndPoint =
                                                nameValueCollection["RepairEndPoint"], 
                                            EquipmentEndPoint =
                                                nameValueCollection["EquipmentEndPoint"], 
                                            DepartmentEndPoint =
                                                nameValueCollection["DepartmentEndPoint"], 
                                            AssignmentEndPoint =
                                                nameValueCollection["AssignmentEndPoint"], 
                                            CustomerEndPoint =
                                                nameValueCollection["CustomerEndPoint"], 
                                            SyncDb = nameValueCollection["SyncDb"], 
                                            SageApiHost = nameValueCollection["SageApiHost"], 
                                            SiteUrl = nameValueCollection["SiteUrl"], 
                                            SertificateUrl =
                                                nameValueCollection["SertificateUrl"], 
                                            SizeBigPhoto =
                                                int.Parse(nameValueCollection["SizeBigPhoto"]), 
                                            SizeSmallPhoto =
                                                int.Parse(nameValueCollection["SizeSmallPhoto"]), 
                                            CurrentTimezone =
                                                nameValueCollection["CurrentTimezone"], 
                                            NotificationDelay =
                                                int.Parse(
                                                    nameValueCollection["NotificationDelay"]), 
                                            CheckTechniciansDelay =
                                                int.Parse(
                                                    nameValueCollection["CheckTechniciansDelay"]), 
                                            SynchronizationDelay =
                                                int.Parse(
                                                    nameValueCollection["SynchronizationDelay"]), 
                                            Connection =
                                                ConfigurationManager.ConnectionStrings[
                                                    "MongoServerSettings"].ConnectionString, 
                                            DbName = ConfigurationManager.AppSettings["MainDb"], 
                                            PureNotificationEnabled =
                                                bool.Parse(
                                                    ConfigurationManager.AppSettings[
                                                        "PureNotificationEnabled"]), 
                                            AlertNotificationEnabled =
                                                bool.Parse(
                                                    ConfigurationManager.AppSettings[
                                                        "AlertNotificationEnabled"]), 
                                            AlertBadgeNotificationEnabled =
                                                bool.Parse(
                                                    ConfigurationManager.AppSettings[
                                                        "AlertBadgeNotificationEnabled"]), 
                                            AlertBadgeSoundNotificationEnabled =
                                                bool.Parse(
                                                    ConfigurationManager.AppSettings[
                                                        "AlertBadgeSoundNotificationEnabled"]), 
                                            NotificationAlert =
                                                ConfigurationManager.AppSettings[
                                                    "NotificationAlert"], 
                                            NotificationBadge =
                                                int.Parse(
                                                    ConfigurationManager.AppSettings[
                                                        "NotificationBadge"]), 
                                            NotificationSound =
                                                ConfigurationManager.AppSettings[
                                                    "NotificationSound"], 
                                            UrlToFolderPhotoWorkOrders =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToFolderPhotoWorkOrders"], 
                                            UrlToFolderPhotoWork =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToFolderPhotoWork"], 
                                            UrlToFolderTecnician =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToFolderTecnician"], 
                                            UrlToFolderWorkOrder =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToFolderWorkOrder"], 
                                            UrlToTechnicianIcon =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToTechnicianIcon"], 
                                            UrlToWorkOrderIcon =
                                                ConfigurationManager.AppSettings[
                                                    "UrlToWorkOrderIcon"],
                                            StorageUrl =
                                                ConfigurationManager.AppSettings[
                                                    "StorageUrl"]
                };

                return configuration;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid settings in web.config", ex);
            }
        }
    }
}