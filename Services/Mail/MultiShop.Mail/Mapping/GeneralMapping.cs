using AutoMapper;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Entities;

namespace MultiShop.Mail.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<SentMail, CreateSentMailDto>().ReverseMap();
            CreateMap<SentMail, ResultSentMailDto>().ReverseMap();
            CreateMap<SentMail, UpdateSentMailDto>().ReverseMap();
            CreateMap<SentMail, GetByIdSentMailDto>().ReverseMap();
        }
    }
}
