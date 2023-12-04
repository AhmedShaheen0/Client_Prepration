using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestPrepation.Data.Models;
using TestPrepation.Data.ViewModels;

namespace TestPrepation.Data.Mappping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Client, ClientViewModel>().ReverseMap();

            CreateMap<Client, ClientViewModel>()
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.MaritalStatus.MaritalStatusName));

            CreateMap<MaritalStatus, SelectListItem>()
              .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.MaritalStatusId))
              .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.MaritalStatusName));

        }
    }
}
