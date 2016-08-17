namespace BloomService.Web.Infrastructure.Utils
{
    public static class PropertyUtil
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
    }
}