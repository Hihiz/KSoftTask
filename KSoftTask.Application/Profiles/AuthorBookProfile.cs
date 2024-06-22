using AutoMapper;
using KSoftTask.Application.Dto.AuthorBooks;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Profiles
{
    public class AuthorBookProfile : Profile
    {
        public AuthorBookProfile()
        {
            CreateMap<AuthorBook, AuthorBookDto>().ReverseMap();
            CreateMap<AuthorBook, CreateAuthorBookDto>().ReverseMap();
            CreateMap<AuthorBook, UpdateAuthorBookDto>().ReverseMap();
        }
    }
}
