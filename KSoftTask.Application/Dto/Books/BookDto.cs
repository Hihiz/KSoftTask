using KSoftTask.Application.Dto.AuthorBooks;
using KSoftTask.Application.Dto.PublisherBooks;

namespace KSoftTask.Application.Dto.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<AuthorBookDto> AuthorBooks { get; } = new List<AuthorBookDto>();
        public ICollection<PublisherBookDto> PublisherBooks { get; } = new List<PublisherBookDto>();
    }
}
