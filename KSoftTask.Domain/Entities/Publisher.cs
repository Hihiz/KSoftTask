namespace KSoftTask.Domain.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<PublisherBook> PublisherBooks { get; } = new List<PublisherBook>();
    }
}
