using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WLVSTools.Web.Core.Data
{
    public class BaseEntities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? CreateUser { get; set; }

        [Required]
        public DateTime? CreateDate { get; set; }

        [Required]
        public string? ModifyUser { get; set; }

        [Required]
        public DateTime? ModifyDate { get; set; }
    }
}
