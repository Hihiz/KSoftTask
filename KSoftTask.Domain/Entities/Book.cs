﻿namespace KSoftTask.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();
        public ICollection<PublisherBook> PublisherBooks { get; } = new List<PublisherBook>();
    }
}
