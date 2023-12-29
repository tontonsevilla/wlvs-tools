using Microsoft.AspNetCore.Html;

namespace WLVSTools.Web.ViewModels.PersonalToolbox
{
    public class LottoViewModel
    {
        public LottoViewModel()
        {
            LottoGame = "0";
        }

        public bool HasFilter { get; set; }

        public string? LottoGame { get; set; }

        public HtmlString? HtmlStringOutput { get; set; }

        public string? StartMonth { get; set; }
        public string? StartDate { get; set; }
        public string? StartYear { get; set;}

        public string? EndMonth { get; set; }
        public string? EndDate { get; set; }
        public string? EndYear { get; set; }
    }
}
