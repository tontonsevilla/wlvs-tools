namespace WLVSTools.Web.WebInfrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToSafeString(this object obj)
        {
            return obj == null ? "" : obj.ToString();
        }
    }
}
