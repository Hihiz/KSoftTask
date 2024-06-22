using KSoftTask.Application.Dto.AuthorBooks;

namespace KSoftTask.Application.Dto.Authors
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorBookDto> AuthorBooks { get; }
    }
}
