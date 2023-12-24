using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Core.Data.PersonalToolsEntities
{
    public class Account : BaseEntities
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? Password { get; set; }

        public void Create(string name, string description, string userId, string password, string modifyUser)
        {
            Name = name;
            Description = description;
            UserId = userId;
            Password = password;
            CreateUser = modifyUser;
            ModifyUser = modifyUser;
            CreateDate = DateTime.Now;
            ModifyDate = DateTime.Now;
        }

        public void Edit(string name, string description, string userId, string password, string modifyUser)
        {
            Name = name;
            Description = description;
            UserId = userId;
            Password = password;
            ModifyUser = modifyUser;
            ModifyDate = DateTime.Now;
        }
    }
}
