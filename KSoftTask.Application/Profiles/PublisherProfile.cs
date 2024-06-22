using AutoMapper;
using KSoftTask.Application.Dto.Publishers;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<Publisher, CreatePublisherDto>().ReverseMap();
            CreateMap<Publisher, UpdatePublisherDto>().ReverseMap();
        }
    }
}
