using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.Core.Data;

namespace WLVSTools.Web.Infrastructure.Lotto
{
    public class LottoResult : BaseEntities
    {
        public string? LottoGame { get; set; }
        public string? Combinations { get; set; }
        public DateTime? DrawDate { get; set; }
        public double? JackpotPrice { get; set; }
        public int? Winners { get; set; }
    }
}
