using AutoMapper;
using WLVSTools.Web.Core.Data.PersonalToolsEntities;
using WLVSTools.Web.ViewModels.PersonalToolbox;

namespace WLVSTools.Web.Models
{
    public class PersonalToolboxAccountProfile : Profile
    {
        public PersonalToolboxAccountProfile()
        {
            CreateMap<Account, AccountViewModel>().ReverseMap();
        }
    }
}
