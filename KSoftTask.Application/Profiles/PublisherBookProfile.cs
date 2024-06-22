using AutoMapper;
using KSoftTask.Application.Dto.PublisherBooks;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Profiles
{
    public class PublisherBookProfile : Profile
    {
        public PublisherBookProfile()
        {
            CreateMap<PublisherBook, PublisherBookDto>().ReverseMap();
            CreateMap<PublisherBook, CreatePublisherBookDto>().ReverseMap();
            CreateMap<PublisherBook, UpdatePublisherBookDto>().ReverseMap();
        }
    }
}
