using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;

namespace WLVSTools.Web.WebInfrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string GetExpressionText<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            var expressionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
                .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

            return expressionProvider.GetExpressionText(expression);
        }

        public static HtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            string datePickerName = htmlHelper.GetExpressionText(expression);
            string datePickerFullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(datePickerName);
            string datePickerID = TagBuilder.CreateSanitizedId(datePickerFullName, "_");

            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            ModelExpressionProvider expressionProvider = new ModelExpressionProvider(htmlHelper.MetadataProvider);
            ModelExpression metadata = expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
            string datePickerValue = (metadata.Model == null ? "" : DateTime.Parse(metadata.Model.ToString()).ToString("yyyy-MM-dd"));

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("name", datePickerFullName);
            tagBuilder.Attributes.Add("id", datePickerID);
            tagBuilder.Attributes.Add("type", "date");
            tagBuilder.Attributes.Add("value", datePickerValue);
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), true);

            //IDictionary<string, object> validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(datePickerFullName, metadata);

            //var modelExplorer = ExpressionMetadataProvider.FromStringExpression(expression, htmlHelper.ViewContext.ViewData, htmlHelper.MetadataProvider);
            //var validator = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<ValidationHtmlAttributeProvider>();
            //validator?.AddAndTrackValidationAttributes(htmlHelper.ViewContext, modelExplorer, expression, tagBuilder.Attributes);

            //foreach (string key in validationAttributes.Keys)
            //{
            //    tagBuilder.Attributes.Add(key, validationAttributes[key].ToString());
            //}

            //return new HtmlString(tagBuilder.se(TagRenderMode.SelfClosing));
            return new HtmlString("");
        }
    }
}
