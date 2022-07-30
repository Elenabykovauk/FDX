using AutoMapper;
using FDX.DataAccess.Models;
using FDX.Services.Models;

namespace FDX.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sms, SmsResponse>()
                .ForMember(d => d.To, s => s.MapFrom(s => s.To.Split()));
            CreateMap<SmsDto, Sms>()
                .ForMember(d => d.To, s => s.MapFrom(s => string.Join(",", s.To)));
        }
    }
}
