using KSoftTask.Application.Dto.PublisherBooks;

namespace KSoftTask.Application.Dto.Publishers
{
    public class PublisherDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<PublisherBookDto> PublisherBooks { get; }
    }
}
