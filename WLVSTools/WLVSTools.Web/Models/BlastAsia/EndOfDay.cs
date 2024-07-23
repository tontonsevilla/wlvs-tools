using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Models.BlastAsia
{
    public class EndOfDay
    {
        [Display(Name = "Steer Account"), Required]
        public string? Email { get; set; } = "";

        [Display(Name = "Steer Password"), Required]
        public string? Password { get; set; } = "";

        [Display(Name = "EOD Subject"), Required]
        public string? Subject { get; set; }

        [Display(Name = "EOD Date"), Required]
        public DateTime? EODDate { get; set; } = DateTime.Now;

        [Display(Name = "EOD Account"), Required]
        public string? EODAccount { get; set; } = "AIFS";
    }
}
