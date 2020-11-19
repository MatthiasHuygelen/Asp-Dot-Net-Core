using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
