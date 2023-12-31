﻿using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.ViewModels.PersonalToolbox
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
