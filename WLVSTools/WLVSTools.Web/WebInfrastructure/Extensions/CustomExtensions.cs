using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;
using System.Web;

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

        public static IHtmlContent CustomValidationSummary(this IHtmlHelper htmlHelper, bool excludePropertyErrors = true)
        {
            if (htmlHelper == null) throw new ArgumentNullException("htmlHelper");

            IHtmlContent validationSummary = null;

            if (!htmlHelper.ViewData.ModelState.IsValid && htmlHelper.ViewData.ModelState.ContainsKey(string.Empty))
            {
                var htmlAttributes = new { @class = "alert alert-danger" };
                validationSummary = htmlHelper.ValidationSummary(excludePropertyErrors, "", htmlAttributes);
            }

            return validationSummary;
        }

        public static string CustomRaw(this IHtmlHelper htmlHelper, IHtmlContent content)
        {
            if (content != null)
            {
                using (var writer = new System.IO.StringWriter())
                {
                    content.WriteTo(writer, HtmlEncoder.Default);
                    return HttpUtility.HtmlDecode(writer.ToString());
                }
            }

            return string.Empty;
        }
    }
}
