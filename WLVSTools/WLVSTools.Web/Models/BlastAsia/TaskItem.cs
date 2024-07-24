using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Models.BlastAsia
{
    public class TaskItem
    {
        [Required]
        public string? Description { get; set; }

        [Display(Name = "No. of Hours"), Required]
        public int? NoOfHours { get; set; }

        [Display(Name = "No. of Minutes"), Required]
        public int? NoOfMinutes { get; set; }
    }
}
