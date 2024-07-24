using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WLVSTools.Web.WebInfrastructure.Extensions
{
    public static class CustomExtensions
    {
        public static void AddErrorMessages(this ModelStateDictionary dictionary, IEnumerable<string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
            {
                dictionary.AddModelError(string.Empty, errorMessage);
            }
        }

        public static void AddErrorMessage(this ModelStateDictionary dictionary, string errorMessage)
        {
            dictionary.AddModelError(string.Empty, errorMessage);
        }
    }
}
