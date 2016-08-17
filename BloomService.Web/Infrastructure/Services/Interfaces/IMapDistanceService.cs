namespace BloomService.Web.Infrastructure.Services.Interfaces
{
    public interface IMapDistanceService
    {
        double Distance(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2);
    }
}
