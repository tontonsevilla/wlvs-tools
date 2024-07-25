using Microsoft.AspNetCore.Hosting.Server;
using System.ComponentModel.DataAnnotations;
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

        public static bool IsValid<T>(T objectToValidate, List<ValidationResult> validationResults)
        {
            if (objectToValidate == null)
                return false;
            
            var context = new ValidationContext(objectToValidate, serviceProvider: null, items: null);
            
            return Validator.TryValidateObject(objectToValidate, context, validationResults, true);
        }
    }
}
