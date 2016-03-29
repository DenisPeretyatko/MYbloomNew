using System.Configuration;

namespace Sage.WebApi.Infratructure.Service.Implementation
{
    public class SageWebConfig
    {
        public string CatalogPath { get; }
        public string ConnectionString { get; }
        public SageWebConfig()
        {
            CatalogPath = ConfigurationManager.AppSettings["catalogPath"];
            ConnectionString = ConfigurationManager.AppSettings["connectionString"];
        }
    }
}