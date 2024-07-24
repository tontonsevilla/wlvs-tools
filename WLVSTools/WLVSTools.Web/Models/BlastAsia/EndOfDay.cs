using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.WebInfrastructure.Attrbutes.Validations;

namespace WLVSTools.Web.Models.BlastAsia
{
    public class EndOfDay
    {
        public EndOfDay()
        {
            TaskItems = new List<TaskItem>();
        }

        [Display(Name = "Steer Account"), Required]
        public string? Email { get; set; }

        [Display(Name = "Steer Password"), Required]
        public string? Password { get; set; }

        [Display(Name = "EOD Subject"), Required]
        public string? Subject { get; set; }

        [Display(Name = "EOD Date"), Required]
        public DateTime? EODDate { get; set; } = DateTime.Now;

        [Display(Name = "EOD Account"), Required]
        public string? EODAccount { get; set; } = "AIFS";

        [Display(Name = "Task Items"), AtLeastOneItemRequiredValidation, CheckItemsValidation]
        public List<TaskItem> TaskItems { get; set; }
    }
}
