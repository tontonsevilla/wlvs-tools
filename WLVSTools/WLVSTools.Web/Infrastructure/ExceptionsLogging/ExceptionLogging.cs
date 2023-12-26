using WLVSTools.Web.Core.Data;

namespace WLVSTools.Web.Infrastructure.ExceptionsLogging
{
    public class ExceptionLogging : BaseEntities
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionUrl { get; set; }
    }
}
