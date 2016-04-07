using System.Web;

namespace BloomService.Web.Utils
{
    public interface ISession
    {
        HttpSessionStateWrapper Session { get; }
    }
}