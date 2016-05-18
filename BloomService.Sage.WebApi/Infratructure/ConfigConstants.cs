namespace Sage.WebApi.Infratructure
{
    using System.Configuration;

    public class SageWebConfig
    {
        public string CatalogPath { get; }
        public string ConnectionString { get; }
        public string TimberlineDataConnectionString { get; private set; }
        public string TimberlineServiceManagementConnectionString { get; private set; }

        public SageWebConfig()
        {
            CatalogPath = ConfigurationManager.AppSettings["catalogPath"];
            TimberlineDataConnectionString = ConfigurationManager.AppSettings["connectionString"];
            TimberlineServiceManagementConnectionString = "DSN=Timberline Service Management;";
        }
    }
}