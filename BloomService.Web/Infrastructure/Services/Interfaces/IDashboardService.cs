using BloomService.Web.Models;

namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface IDashboardService
    {
        LookupsModel GetLookups();
    }
}