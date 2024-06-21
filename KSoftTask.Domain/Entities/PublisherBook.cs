namespace KSoftTask.Domain.Entities
{
    public class PublisherBook
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
