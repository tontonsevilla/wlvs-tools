using Microsoft.AspNetCore.Hosting.Server;
using System.Reflection;

namespace WLVSTools.Web.WebInfrastructure.General
{
    public static class Helper
    {
        public static string GetWebRoot(HttpContext httpContext)
        {
            var server = httpContext.RequestServices.GetRequiredService<IServer>();
            PropertyInfo? pi = server.GetType().GetProperty("VirtualPath");

            return (pi?.GetValue(server) as string)?.ToLower();
        }
    }
}
