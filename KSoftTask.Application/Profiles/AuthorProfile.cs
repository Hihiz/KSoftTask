using AutoMapper;
using KSoftTask.Application.Dto.Authors;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, UpdateAuthorDto>().ReverseMap();
        }
    }
}
