using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Models.BlastAsia
{
    public class TimeKeeping
    {
        public TimeKeeping()
        {
            Body = string.Empty;
        }
        public TimeKeeping(TimeKeepingType timeKeepingType)
        {
            Body = string.Empty;
            Type = timeKeepingType;

            switch (timeKeepingType)
            {
                case TimeKeepingType.In:
                    Body = "IM IN";
                    break;
                case TimeKeepingType.Out:
                    Body = "IM OUT";
                    break;
                case TimeKeepingType.Unset:
                default:
                    break;
            }
        }

        public TimeKeepingType Type { get; set; }

        [Display(Name = "Email Recipient")]
        public string Recipient { get; set; } = "attendance@blastasia.com";

        [Display(Name = "Email CC Recipient")]
        public string CCRecipient { get; set; } = "rfernandez@blastasia.com";

        [Display(Name = "Email Subject")]
        public string Subject { get; set; } = $"WFH {DateTime.Now.ToString("MM/dd/yyyy")}";

        [Display(Name = "Email Date"), DataType(DataType.Date)]
        public DateTime? EmailDate { get; set; } = DateTime.Now;

        [Display(Name = "Email Body")]
        public string Body { get; set; }
    }

    public enum TimeKeepingType
    {
        Unset,
        In,
        Out
    }
}
