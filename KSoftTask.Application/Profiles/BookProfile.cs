using AutoMapper;
using KSoftTask.Application.Dto.Books;
using KSoftTask.Domain.Entities;

namespace KSoftTask.Application.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
        }    
    }
}
