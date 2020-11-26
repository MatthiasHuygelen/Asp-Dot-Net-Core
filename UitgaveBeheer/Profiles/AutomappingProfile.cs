using AutoMapper;
using UitgaveBeheer.Models;

namespace UitgaveBeheer.Profiles
{
    public class AutomappingProfile : Profile
    {
        public AutomappingProfile()
        {
            CreateMap<ExpenseDetailDto, ExpenseDetailViewModel>().ReverseMap();
            CreateMap<ExpenseDetailDto, ExpenseEditViewModel>().ReverseMap();
        }
    }
}
